using Digital.Lib.Net.Authentication;
using Digital.Lib.Net.Files;
using Digital.Lib.Net.Mvc;
using Digital.Lib.Net.Sdk;
using Digital.Pages.Api.Data;

namespace Digital.Pages.Api;

public sealed class Program
{
    private const string AppName = "Digital.Pages.Api";

    private static async Task Main(string[] args)
    {
        var app = WebApplication.CreateBuilder(args)
            .AddDigitalSdk(AppName)
            .AddDigitalAuthentication()
            .AddDigitalFilesServices()
            .AddDigitalMvc()
            .AddPagesContext()
            .Build();

        await app
            .UseDigitalSdk(AppName)
            .RunAsync();
    }
}