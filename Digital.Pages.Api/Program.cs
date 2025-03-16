using Digital.Lib.Net.Authentication;
using Digital.Lib.Net.Files;
using Digital.Lib.Net.Mvc;
using Digital.Lib.Net.Sdk;
using Digital.Lib.Net.Sdk.Services.Application;
using Digital.Pages.Api.Data;

namespace Digital.Pages.Api;

public sealed class Program
{
    private static async Task Main(string[] args)
    {
        var app = WebApplication.CreateBuilder(args)
            .SetApplicationName("Digital.Pages.Api")
            .AddDigitalSdk()
            .AddDigitalAuthentication()
            .AddDigitalFilesServices()
            .AddDigitalMvc()
            .AddPagesContext()
            .Build();

        await app
            .UseDigitalSdk()
            .RunAsync();
    }
}