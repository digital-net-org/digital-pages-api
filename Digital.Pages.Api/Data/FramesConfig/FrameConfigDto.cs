using Digital.Lib.Net.Entities.Models.Documents;

namespace Digital.Pages.Api.Data.FramesConfig;

public class FrameConfigDto
{
    public FrameConfigDto() { }

    public FrameConfigDto(FrameConfig frameConfig, Document document)
    {
        Id = frameConfig.Id;
        Version = frameConfig.Version;
        IsPublished = frameConfig.IsPublished;
        Document = new DocumentDto(document);
    }

    public int Id { get; set; }
    public string Version { get; set; } = string.Empty;
    public bool IsPublished { get; set; }
    public DocumentDto? Document { get; set; }
}