using Digital.Pages.Api.Data.Views;

namespace Digital.Pages.Api.Services;

public interface IViewService
{
    public ViewPublicDto? GetPublicView(string viewPath);
}