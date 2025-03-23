using Digital.Lib.Net.Core.Messages;
using Digital.Pages.Api.Data.FramesConfig;

namespace Digital.Pages.Api.Services;

public interface IFrameConfigValidationService
{
    public Task<Result> ValidateUpload(IFormFile file, string version);
    public Task<Result<FrameConfig>> GetDeletable(int id);
}