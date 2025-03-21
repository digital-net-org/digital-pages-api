using Digital.Lib.Net.Authentication.Attributes;
using Digital.Lib.Net.Core.Messages;
using Digital.Lib.Net.Entities.Services;
using Digital.Lib.Net.Mvc.Controllers.Crud;
using Digital.Pages.Api.Data;
using Digital.Pages.Api.Data.Views;
using Microsoft.AspNetCore.Mvc;

namespace Digital.Pages.Api.Controllers.ViewApi;

[ApiController, Route("view"), Authorize(AuthorizeType.Any)]
public class ViewController(
    IEntityService<View, DigitalPagesContext> viewService
) : CrudController<View, DigitalPagesContext, ViewDto, ViewPayload>(viewService);