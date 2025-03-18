using System.Linq.Expressions;
using Digital.Lib.Net.Authentication.Attributes;
using Digital.Lib.Net.Core.Predicates;
using Digital.Lib.Net.Entities.Repositories;
using Digital.Lib.Net.Mvc.Controllers.Pagination;
using Digital.Pages.Api.Data;
using Digital.Pages.Api.Data.FramesConfig;
using Microsoft.AspNetCore.Mvc;

namespace Digital.Pages.Api.Controllers.FrameConfigApi;

[ApiController, Route("frame/config"), Authorize(AuthorizeType.Any)]
public class ConfigPaginationController(
    IRepository<FrameConfig, DigitalPagesContext> frameRepository
) : PaginationController<FrameConfig, DigitalPagesContext, FrameConfigDto, FrameConfigQuery>(frameRepository)
{
    protected override Expression<Func<FrameConfig, bool>> Filter(
        Expression<Func<FrameConfig, bool>> predicate, FrameConfigQuery query
    )
    {
        if (!string.IsNullOrEmpty(query.Version))
            predicate = predicate.Add(x => x.Version.StartsWith(query.Version));
        if (query.IsPublished is not null)
            predicate = predicate.Add(x => x.IsPublished == query.IsPublished);
        return predicate;
    }
}