using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OGWeb.Infrastructure.Context;

namespace OGWeb.Infrastructure;

public static class ServiceExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("SqlConnection");
        services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString),
             configure =>
             {
                 configure.MigrationsAssembly("MediaBalans.Persistence");
             })
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors()
            .ConfigureWarnings(warnings =>
            {
                warnings.Ignore(CoreEventId.LazyLoadOnDisposedContextWarning);
            }));


        return services;
    }
}
