using Digital.Lib.Net.Authentication.Attributes;
using Digital.Lib.Net.Core.Exceptions;
using Digital.Lib.Net.Core.Messages;
using Digital.Lib.Net.Entities.Services;
using Digital.Pages.Api.Data;
using Digital.Pages.Api.Data.FramesConfig;
using Digital.Pages.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Digital.Pages.Api.Controllers.ConfigApi;

[ApiController, Route("config"), Authorize(AuthorizeType.Any)]
public class ConfigController(
    IEntityService<FrameConfig, DigitalPagesContext> frameService,
    IFrameConfigService frameConfigService
) : ControllerBase
{

    [HttpGet("{id:int}")]
    public ActionResult GetConfig(int id)
    {
        var result = frameConfigService.GetConfig(x => x.Id == id);
        return result.HasError() ? NotFound() : result.Value!;
    }

    [HttpGet("validate")]
    public ActionResult<Result> GetConfigStatus() => frameConfigService.GetConfigStatus();

    [HttpPut("publish")]
    public async Task<ActionResult<Result>> PublishConfig(int id)
    {
        var result = await frameConfigService.Publish(id);
        return result.HasError() ? BadRequest(result) : Ok(result);
    }

    [HttpPost("upload")]
    public async Task<ActionResult<Result<FrameConfigDto>>> UploadConfig(IFormFile file, [FromForm] string version)
    {
        var result = await frameConfigService.Upload(file, version);
        if (result.HasError<ResourceDuplicateException>())
            return Conflict(result);
        if (result.HasError())
            return BadRequest(result);

        return Ok(result);
    }

};