using Digital.Lib.Net.Entities.Models.Documents;

namespace Digital.Pages.Api.Data.FramesConfig;

public class FrameConfigDto
{
    public FrameConfigDto() { }

    public FrameConfigDto(FrameConfig frameConfig)
    {
        Id = frameConfig.Id;
        Version = frameConfig.Version;
        CreatedAt = frameConfig?.CreatedAt;
    }

    public FrameConfigDto(FrameConfig frameConfig, Document document)
    {
        Id = frameConfig.Id;
        Version = frameConfig.Version;
        CreatedAt = frameConfig.CreatedAt;
        UpdatedAt = frameConfig.UpdatedAt;
    }

    public int Id { get; set; }
    public string Version { get; set; } = string.Empty;
    public DateTime? CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}