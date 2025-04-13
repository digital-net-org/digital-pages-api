using System.Linq.Expressions;
using Digital.Lib.Net.Core.Messages;
using Digital.Lib.Net.Entities.Models.Users;
using Digital.Pages.Api.Data.FramesConfig;
using Microsoft.AspNetCore.Mvc;

namespace Digital.Pages.Api.Services;

public interface IFrameConfigService
{
    Task<Result<FrameConfigDto>> UploadAsync(IFormFile file, string version, User uploader);
    Result<FileResult> GetConfig(string version);
    Task<Result> DeleteAsync(int id);
}