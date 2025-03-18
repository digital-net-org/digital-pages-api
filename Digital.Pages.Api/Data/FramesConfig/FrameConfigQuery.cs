using Digital.Lib.Net.Mvc.Controllers.Pagination;

namespace Digital.Pages.Api.Data.FramesConfig;

public class FrameConfigQuery : Query
{
    public string? Version { get; set; }
    public bool? IsPublished { get; set; }
}