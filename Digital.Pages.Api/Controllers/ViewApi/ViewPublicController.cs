using Digital.Pages.Api.Data.Views;
using Digital.Pages.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Digital.Pages.Api.Controllers;

[ApiController, Route("view/public")]
public class ViewPublicController(
    IFrameConfigService frameConfigService,
    IViewService viewService
) : ControllerBase
{
    [HttpGet("{*path}")]
    public ActionResult<View> GetPublicView(string path)
    {
        var result = viewService.GetPublicView(path);
        return result is not null ? Ok(result) : NotFound();
    }

    [HttpGet("config/{version}")]
    public ActionResult GetConfig(string version)
    {
        var result = frameConfigService.GetConfig(version);
        return result.HasError() ? NotFound() : result.Value!;
    }
}