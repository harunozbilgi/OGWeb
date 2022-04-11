using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OGWeb.Core.Settings;

namespace OGWeb.Core;

public static class ServiceExtensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        return services;
    }
}