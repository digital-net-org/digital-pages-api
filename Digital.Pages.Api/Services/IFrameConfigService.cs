using System.Linq.Expressions;
using Digital.Lib.Net.Core.Messages;
using Digital.Pages.Api.Data.FramesConfig;
using Microsoft.AspNetCore.Mvc;

namespace Digital.Pages.Api.Services;

public interface IFrameConfigService
{
    Task<Result> Publish(int id);
    Task<Result<FrameConfigDto>> Upload(IFormFile file, [FromForm] string version);
    Result<FileResult> GetConfig(Expression<Func<FrameConfig, bool>> expression);
    Result GetConfigStatus();
}