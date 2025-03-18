using Digital.Lib.Net.Entities.Repositories;
using Digital.Pages.Api.Data;
using Digital.Pages.Api.Data.Views;

namespace Digital.Pages.Api.Services;

public class ViewService(
    IRepository<View, DigitalPagesContext> viewRepository
) : IViewService
{
    public ViewPublicDto? GetPublicView(string viewPath)
    {
        var view = viewRepository
            .Get(v => v.Path == viewPath && v.IsPublished)
            .FirstOrDefault();
        return view?.Frame?.Data is null ? null : new ViewPublicDto(view);
    }
}