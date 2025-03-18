using Digital.Lib.Net.Core.Messages;
using Digital.Pages.Api.Data.Views;
using Digital.Pages.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Digital.Pages.Api.Controllers;

[ApiController, Route("public")]
public class ViewPublicController(
    IFrameConfigService frameConfigService,
    IViewService viewService
) : ControllerBase
{
    [HttpGet("view/{*path}")]
    public ActionResult<View> GetPublicView(string path)
    {
        var result = viewService.GetPublicView(path);
        return result is not null ? Ok(result) : NotFound();
    }

    [HttpGet("config/published")]
    public ActionResult GetPublishedConfig()
    {
        var result = frameConfigService.GetConfig(x => x.IsPublished);
        return result.HasError() ? NotFound() : result.Value!;
    }
}