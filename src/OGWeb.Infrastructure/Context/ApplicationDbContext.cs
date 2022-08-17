using System.Reflection;
using Microsoft.EntityFrameworkCore;
using OGWeb.Core.Entities;
using OGWeb.Infrastructure.Configurations;

namespace OGWeb.Infrastructure.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public DbSet<Work> Works { get; set; }
    public DbSet<AppSeo> AppSeos { get; set; }
    public DbSet<AppSetting> AppSettings { get; set; }
    public DbSet<OverView> OverViews { get; set; }
    public DbSet<Video> Videos { get; set; }
    public DbSet<WorkFile> WorkFiles { get; set; }
    public DbSet<Slider> Sliders { get; set; }
}
