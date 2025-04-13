using Digital.Lib.Net.Core.Exceptions;
using Digital.Lib.Net.Core.Messages;
using Digital.Lib.Net.Entities.Repositories;
using Digital.Pages.Api.Data;
using Digital.Pages.Api.Data.FramesConfig;
using Digital.Pages.Api.Exceptions;

namespace Digital.Pages.Api.Services;

public class FrameConfigValidationService(
    IRepository<FrameConfig, DigitalPagesContext> frameConfigRepository
) : IFrameConfigValidationService
{
    private static readonly List<string> AllowedMimeTypes =
        ["application/javascript", "text/javascript", "application/x-javascript"];

    public async Task<Result> ValidateUpload(IFormFile file, string version)
    {
        var result = new Result();
        if (await frameConfigRepository.CountAsync(x => x.Version.ToLower() == version.ToLower()) > 0)
            return result.AddError(new ResourceDuplicateException());
        if (file.Length == 0)
            return result.AddError(new ResourceMalformedException());
        if (!AllowedMimeTypes.Contains(file.ContentType))
            return result.AddError(new ResourceContentTypeException());
        if (string.IsNullOrEmpty(file.FileName))
            return result.AddError(new ResourceMalformedException());
        return result;
    }

    public async Task<Result<FrameConfig>> GetDeletable(int id)
    {
        var result = new Result<FrameConfig>();
        var config = await frameConfigRepository.GetByIdAsync(id);
        if (config is null)
            return result.AddError(new ResourceNotFoundException());
        if (config.Frames.Count > 0)
            return result.AddError(new CannotDeletePublishedConfigException(config.Id));
        result.Value = config;
        return result;
    }
}