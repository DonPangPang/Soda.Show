
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Soda.Show.WebApi.Data;
using Soda.Show.WebApi.Services;

namespace Soda.Show.WebApi.Extensions;

public static class GlobalUsings
{
    public static void AddDb(this IServiceCollection services)
    {
        services.AddDbContext<SodaDbContext>(opts =>
        {
            opts.UseSqlite("Data source=soda.db");
            //opts.UseSqlite("Data Source=soda.db");
            opts.ReplaceService<IMigrationsSqlGenerator, CustomMigrationsSqlGenerator>();
        });
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        var types = AppDomain.CurrentDomain
            .GetAssemblies()
            .SelectMany(x => x.GetTypes())
            .Where(t => t is { IsClass: true, IsAbstract: false }
                        && t.GetCustomAttributes(typeof(ServiceAttribute), false).Length > 0
            )
            .ToList();

        types.ForEach(impl =>
            {
                //获取该类所继承的所有接口
                var interfaces = impl.GetInterfaces();
                //获取该类注入的生命周期
                var lifetime = impl.GetCustomAttribute<ServiceAttribute>()?.LifeTime;

                interfaces.ToList().ForEach(i =>
                {
                    switch (lifetime)
                    {
                        case ServiceLifetime.Singleton:
                            services.AddSingleton(i, impl);
                            break;

                        case ServiceLifetime.Scoped:
                            services.AddScoped(i, impl);
                            break;

                        case ServiceLifetime.Transient:
                            services.AddTransient(i, impl);
                            break;
                    }
                });
            });

        return services;
    }
}