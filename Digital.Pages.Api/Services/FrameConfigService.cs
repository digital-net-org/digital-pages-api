using System.Linq.Expressions;
using Digital.Lib.Net.Core.Exceptions;
using Digital.Lib.Net.Core.Messages;
using Digital.Lib.Net.Entities.Context;
using Digital.Lib.Net.Entities.Models.Documents;
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
    public Result<FileResult> GetConfig(Expression<Func<FrameConfig, bool>> expression)
    {
        var result = new Result<FileResult>();
        var config = frameConfigRepository.Get(expression).FirstOrDefault();
        if (config is null)
            return result.AddError(new NoPublishedConfigException());

        result.Value = documentService.GetDocumentFile(config.DocumentId, "application/javascript");
        if (result.Value is null)
            result.AddError(new NoPublishedConfigFileException(config.Id));

        return result;
    }

    public Result GetConfigStatus()
    {
        var result = new Result();
        var config = frameConfigRepository.Get(x => x.IsPublished).FirstOrDefault();
        if (config is null)
            result.AddError(new NoPublishedConfigException());
        return result;
    }

    public async Task<Result<FrameConfigDto>> Upload(IFormFile file, [FromForm] string version)
    {
        var result = new Result<FrameConfigDto>();
        if (result.Merge(await frameConfigValidationService.ValidateUpload(file, version)).HasError())
            return result;

        var documentResult = await documentService.SaveDocumentAsync(file);
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

    public async Task<Result> Publish(int id)
    {
        var result = new Result();

        var config = await frameConfigRepository.GetByIdAsync(id);
        if (config is null)
            return result.AddError(new ResourceNotFoundException());
        if (config is { IsPublished: true })
            return result.AddInfo("This resource is already published. Nothing to do.");
        config.IsPublished = true;
        try
        {
            var current = frameConfigRepository.Get(x => x.IsPublished);
            foreach (var cfg in frameConfigRepository.Get(x => x.IsPublished))
                cfg.IsPublished = false;

            frameConfigRepository.UpdateRange([..current, config]);
            await frameConfigRepository.SaveAsync();
        }
        catch (Exception ex)
        {
            result.AddError(ex);
        }
        return result;
    }
}