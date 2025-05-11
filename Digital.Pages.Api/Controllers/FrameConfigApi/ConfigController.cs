using Digital.Lib.Net.Authentication.Attributes;
using Digital.Lib.Net.Authentication.Services.Authentication;
using Digital.Lib.Net.Core.Exceptions;
using Digital.Lib.Net.Core.Messages;
using Digital.Lib.Net.Entities.Models.Users;
using Digital.Lib.Net.Entities.Repositories;
using Digital.Lib.Net.Http.HttpClient.Extensions;
using Digital.Pages.Api.Data;
using Digital.Pages.Api.Data.FramesConfig;
using Digital.Pages.Api.Exceptions;
using Digital.Pages.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Digital.Pages.Api.Controllers.FrameConfigApi;

[ApiController, Route("frame/config"), Authorize(AuthorizeType.Any)]
public class ConfigController(
    IFrameConfigService frameConfigService,
    IRepository<FrameConfig, DigitalPagesContext> frameConfigRepository,
    IUserContextService userContextService
) : ControllerBase
{
    [HttpGet("test")]
    public async Task<ActionResult<Result>> Test()
    {
        var result = new Result();
        if (await frameConfigRepository.CountAsync(_ => true) is 0)
            result.AddError(new NoFrameConfigException());
        
        return result.HasErrorOfType<NoFrameConfigException>() ? Conflict(result) : Ok(result);
    }
    
    [HttpGet("{version}")]
    public ActionResult GetConfigFile(string version)
    {
        var result = new Result();
        var config = result.Try(() => frameConfigService.GetConfig(version));
        var etag = config?.DocumentId.ToString();
        
        if (result.HasError || config is null)
            return NotFound();
        if (Request.Headers.TestIfNoneMatch(etag))
            return StatusCode(304);

        var file = result.Try(() => frameConfigService.GetConfigFile(config));
        if (result.HasError || file is null)
            return NotFound();
        
        Response.Headers.CacheControl = "public, max-age=0, must-revalidate";
        Response.Headers.ETag = etag;
        Response.Headers.Remove("Content-Disposition");
        return file;
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Result<FrameConfigDto>>> GetConfig(int id)
    {
        var result = new Result<FrameConfigDto>();
        var config = await frameConfigRepository.GetByIdAsync(id);
        if (config is null)
            return NotFound(result.AddError(new ResourceNotFoundException()));
        result.Value = new FrameConfigDto(config);
        return Ok(result);
    }

    [HttpPost("upload")]
    public async Task<ActionResult<Result<FrameConfigDto>>> UploadConfig(IFormFile file, [FromForm] string version)
    {
        var user = userContextService.GetUser();
        var result = await frameConfigService.UploadAsync(file, version, user);
        if (result.HasErrorOfType<ResourceDuplicateException>())
            return Conflict(result);
        if (result.HasError)
            return BadRequest(result);
        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Result>> DeleteConfig(int id)
    {
        var result = await frameConfigService.DeleteAsync(id);
        if (result.HasErrorOfType<ResourceNotFoundException>())
            return NotFound(result);
        if (result.HasErrorOfType<CannotDeletePublishedConfigException>())
            return BadRequest(result);
        if (result.HasError)
            return StatusCode(500, result);
        return Ok(result);
    }
};