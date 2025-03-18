using Digital.Lib.Net.Authentication;
using Digital.Lib.Net.Files;
using Digital.Lib.Net.Mvc;
using Digital.Lib.Net.Sdk;
using Digital.Lib.Net.Sdk.Services.Application;
using Digital.Pages.Api.Data;
using Digital.Pages.Api.Services;

namespace Digital.Pages.Api;

public sealed class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args)
            .SetApplicationName("Digital.Pages.Api")
            .AddDigitalSdk()
            .AddDigitalAuthentication()
            .AddDigitalFilesServices()
            .AddDigitalMvc()
            .AddPagesContext();

        builder.Services
            .AddScoped<IFrameConfigValidationService, FrameConfigValidationService>()
            .AddScoped<IFrameConfigService, FrameConfigService>()
            .AddScoped<IViewService, ViewService>();

        await builder
            .Build()
            .UseDigitalSdk()
            .RunAsync();
    }
}