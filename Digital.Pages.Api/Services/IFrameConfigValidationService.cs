using Digital.Lib.Net.Core.Messages;

namespace Digital.Pages.Api.Services;

public interface IFrameConfigValidationService
{
    public Task<Result> ValidateUpload(IFormFile file, string version);
}