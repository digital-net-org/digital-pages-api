using Digital.Lib.Net.Core.Exceptions;
using Digital.Lib.Net.Core.Messages;
using Digital.Lib.Net.Entities.Repositories;
using Digital.Pages.Api.Data;
using Digital.Pages.Api.Data.FramesConfig;

namespace Digital.Pages.Api.Services;

public class FrameConfigValidationService(
    IRepository<FrameConfig, DigitalPagesContext> frameConfigRepository
) : IFrameConfigValidationService
{
    public async Task<Result> ValidateUpload(IFormFile file, string version)
    {
        var result = new Result();
        if (await frameConfigRepository.CountAsync(x => x.Version == version) > 0)
            return result.AddError(new ResourceDuplicateException());
        if (file.Length == 0)
            return result.AddError(new ResourceMalformedException());
        if (file.ContentType is not "application/javascript")
            return result.AddError(new ResourceContentTypeException());
        if (string.IsNullOrEmpty(file.FileName))
            return result.AddError(new ResourceMalformedException());
        return result;
    }
}