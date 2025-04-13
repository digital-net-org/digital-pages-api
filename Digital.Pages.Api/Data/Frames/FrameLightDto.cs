namespace Digital.Pages.Api.Data.Frames;

public class FrameLightDto
{
    public FrameLightDto()
    {
    }

    public FrameLightDto(Frame frame)
    {
        Id = frame.Id;
        Name = frame.Name;
        Version = frame.Config?.Version ?? string.Empty;
        ConfigId = frame.ConfigId;
        CreatedAt = frame.CreatedAt;
        UpdatedAt = frame.UpdatedAt;
    }

    public Guid? Id { get; init; }
    public string Name { get; set; }
    public string Version { get; set; }
    public int ConfigId { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}