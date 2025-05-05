using Digital.Lib.Net.Authentication.Attributes;
using Digital.Lib.Net.Core.Messages;
using Digital.Lib.Net.Core.Models;
using Digital.Lib.Net.Entities.Repositories;
using Digital.Lib.Net.Entities.Services;
using Digital.Lib.Net.Mvc.Controllers.Crud;
using Digital.Pages.Api.Data;
using Digital.Pages.Api.Data.Frames;
using Digital.Pages.Api.Data.FramesConfig;
using Digital.Pages.Api.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Digital.Pages.Api.Controllers.FrameApi;

[ApiController, Route("frame"), Authorize(AuthorizeType.Any)]
public class FrameController(
    IEntityService<Frame, DigitalPagesContext> frameService,
    IRepository<FrameConfig, DigitalPagesContext> frameConfigRepository
) : CrudController<Frame, DigitalPagesContext, FrameDto, FramePayload>(frameService)
{
    private readonly IEntityService<Frame, DigitalPagesContext> _frameService = frameService;

    [HttpPost("")]
    public override async Task<ActionResult<Result>> Post([FromBody] FramePayload payload)
    {
        if (payload.ConfigId == 0)
        {
            var defaultVersion = frameConfigRepository
                .Get()
                .OrderByDescending(x => x.CreatedAt)
                .FirstOrDefault()?.Id;
            if (defaultVersion is null)
                return BadRequest(new Result().AddError(new NoFrameConfigException()));
            
            payload.ConfigId = (int)defaultVersion;
        }
        var result = await _frameService.Create(Mapper.Map<FramePayload, Frame>(payload));
        return result.HasError() ? BadRequest(result) : Ok(result);
    }
};