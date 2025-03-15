using Digital.Lib.Net.Entities;
using Digital.Lib.Net.Sdk.Bootstrap;
using Digital.Pages.Api.Data.Frames;
using Digital.Pages.Api.Data.Views;

namespace Digital.Pages.Api.Data;

public static class DataInjector
{
    public static WebApplicationBuilder AddPagesContext(this WebApplicationBuilder builder)
    {
        builder
            .AddDatabaseContext<DigitalPagesContext>()
            .ApplyMigrations<DigitalPagesContext>();
        builder.Services
            .AddDigitalEntities<Frame>()
            .AddDigitalEntities<View>();
        return builder;
    }
}
