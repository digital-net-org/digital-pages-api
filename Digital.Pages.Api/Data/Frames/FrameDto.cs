using Digital.Pages.Api.Data.FramesConfig;

namespace Digital.Pages.Api.Data.Frames;

public class FrameDto
{
    public FrameDto()
    {
    }

    public FrameDto(Frame frame)
    {
        Id = frame.Id;
        Name = frame.Name;
        Data = frame.Data;
        Config = new FrameConfigDto(frame.Config);
        CreatedAt = frame.CreatedAt;
        UpdatedAt = frame.UpdatedAt;
    }

    public Guid? Id { get; init; }
    public string Name { get; set; }
    public string? Data { get; set; }
    public FrameConfigDto Config { get; set; }
    public DateTime? CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}