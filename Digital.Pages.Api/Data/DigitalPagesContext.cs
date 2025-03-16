using Digital.Pages.Api.Data.Frames;
using Digital.Pages.Api.Data.Views;
using Microsoft.EntityFrameworkCore;

namespace Digital.Pages.Api.Data;

public class DigitalPagesContext(DbContextOptions<DigitalPagesContext> options) : DbContext(options)
{
    public const string Schema = "digital_pages";

    public DbSet<Frame> Frames { get; init; }
    public DbSet<View> Views { get; init; }

    protected override void OnModelCreating(ModelBuilder builder) => builder.HasDefaultSchema(Schema);
}
