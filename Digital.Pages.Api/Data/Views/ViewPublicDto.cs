namespace Digital.Pages.Api.Data.Views;

public class ViewPublicDto
{
    public ViewPublicDto()
    {
    }

    public ViewPublicDto(View view)
    {
        Data = view.Frame?.Data;
        Version = view.Frame?.Config.Version;
    }

    public string? Data { get; set; }
    public string? Version { get; set; }
}