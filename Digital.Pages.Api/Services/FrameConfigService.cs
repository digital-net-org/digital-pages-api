using Digital.Lib.Net.Core.Exceptions;
using Digital.Lib.Net.Core.Messages;
using Digital.Lib.Net.Entities.Models.Users;
using Digital.Lib.Net.Entities.Repositories;
using Digital.Lib.Net.Files.Services;
using Digital.Pages.Api.Data;
using Digital.Pages.Api.Data.FramesConfig;
using Digital.Pages.Api.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Digital.Pages.Api.Services;

public class FrameConfigService(
    IRepository<FrameConfig, DigitalPagesContext> frameConfigRepository,
    IFrameConfigValidationService frameConfigValidationService,
    IDocumentService documentService
) : IFrameConfigService
{
    public Result<FileResult> GetConfig(string version)
    {
        var result = new Result<FileResult>();
        var config = frameConfigRepository.Get(x => x.Version == version).FirstOrDefault();
        if (config is null)
            return result.AddError(new ResourceNotFoundException());

        result.Value = documentService.GetDocumentFile(config.DocumentId, "application/javascript");
        if (result.Value is null)
            result.AddError(new NoFrameConfigFileException(config.Id));

        return result;
    }

    public async Task<Result<FrameConfigDto>> UploadAsync(IFormFile file, string version, User uploader)
    {
        var result = new Result<FrameConfigDto>();
        if (result.Merge(await frameConfigValidationService.ValidateUpload(file, version)).HasError())
            return result;

        var documentResult = await documentService.SaveDocumentAsync(file, uploader);
        result.Merge(documentResult);

        if (result.HasError() || documentResult.Value is null)
            return result;

        var config = new FrameConfig
        {
            DocumentId = documentResult.Value.Id,
            Version = version,
        };
        await frameConfigRepository.CreateAndSaveAsync(config);

        result.Value = new FrameConfigDto(config, documentResult.Value);
        return result;
    }

    public async Task<Result> DeleteAsync(int id)
    {
        var result = await frameConfigValidationService.GetDeletable(id);
        if (result.HasError())
            return result;
        try
        {
            await documentService.RemoveDocumentAsync(result.Value!.DocumentId);
            frameConfigRepository.Delete(result.Value);
            await frameConfigRepository.SaveAsync();
        }
        catch (Exception ex)
        {
            result.AddError(ex);
        }
        return result;
    }
}