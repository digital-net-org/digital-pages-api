using Digital.Lib.Net.Authentication.Attributes;
using Digital.Lib.Net.Authentication.Services.Authentication;
using Digital.Lib.Net.Core.Exceptions;
using Digital.Lib.Net.Core.Messages;
using Digital.Lib.Net.Entities.Repositories;
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
        if (result.HasError<ResourceDuplicateException>())
            return Conflict(result);
        if (result.HasError())
            return BadRequest(result);
        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Result>> DeleteConfig(int id)
    {
        var result = await frameConfigService.DeleteAsync(id);
        if (result.HasError<ResourceNotFoundException>())
            return NotFound(result);
        if (result.HasError<CannotDeletePublishedConfigException>())
            return BadRequest(result);
        if (result.HasError())
            return StatusCode(500, result);
        return Ok(result);
    }
};