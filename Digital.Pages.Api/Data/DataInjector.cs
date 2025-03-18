using Digital.Lib.Net.Entities;
using Digital.Lib.Net.Entities.Repositories;
using Digital.Lib.Net.Entities.Services;
using Digital.Lib.Net.Sdk.Bootstrap;
using Digital.Pages.Api.Data.Frames;
using Digital.Pages.Api.Data.FramesConfig;
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
            .AddScoped<IRepository<Frame, DigitalPagesContext>, Repository<Frame, DigitalPagesContext>>()
            .AddScoped<IEntityService<Frame, DigitalPagesContext>, EntityService<Frame, DigitalPagesContext>>()
            .AddScoped<IRepository<FrameConfig, DigitalPagesContext>, Repository<FrameConfig, DigitalPagesContext>>()
            .AddScoped<IEntityService<FrameConfig, DigitalPagesContext>, EntityService<FrameConfig, DigitalPagesContext>>()
            .AddScoped<IRepository<View, DigitalPagesContext>, Repository<View, DigitalPagesContext>>()
            .AddScoped<IEntityService<View, DigitalPagesContext>, EntityService<View, DigitalPagesContext>>();
        return builder;
    }
}
