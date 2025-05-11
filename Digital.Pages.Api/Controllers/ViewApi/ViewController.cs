using Digital.Lib.Net.Authentication.Attributes;
using Digital.Lib.Net.Core.Messages;
using Digital.Lib.Net.Entities.Services;
using Digital.Lib.Net.Mvc.Controllers.Crud;
using Digital.Pages.Api.Data;
using Digital.Pages.Api.Data.Views;
using Digital.Pages.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Digital.Pages.Api.Controllers.ViewApi;

[ApiController, Route("view"), Authorize(AuthorizeType.Any)]
public class ViewController(
    IViewService viewService,
    IEntityService<View, DigitalPagesContext> viewEntityService
) : CrudController<View, DigitalPagesContext, ViewDto, ViewPayload>(viewEntityService)
{
    [HttpGet("{*path}")]
    public ActionResult<View> GetPublicView(string path)
    {
        var result = viewService.GetPublicView(path);
        return result is not null ? Ok(result) : NotFound();
    }
}