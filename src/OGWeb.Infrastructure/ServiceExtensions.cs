using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OGWeb.Core.Interfaces.Repositories;
using OGWeb.Infrastructure.Context;
using OGWeb.Infrastructure.Repositories;
namespace OGWeb.Infrastructure;

public static class ServiceExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("SqlConnection");
        services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString),
             configure =>
             {
                 configure.MigrationsAssembly("OGWeb.Infrastructure");
             })
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors()
            .ConfigureWarnings(warnings =>
            {
                warnings.Ignore(CoreEventId.LazyLoadOnDisposedContextWarning);
            }));
        services.AddScoped<IWriteRepositoryManager, WriteRepositoryManager>();
        services.AddScoped<IReadRepositoryManager, ReadRepositoryManager>();
        return services;
    }
}
