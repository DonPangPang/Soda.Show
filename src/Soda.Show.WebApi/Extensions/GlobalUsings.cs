
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
            opts.UseSqlite("Data Source=soda.db");
            opts.ReplaceService<IMigrationsSqlGenerator, CustomMigrationsSqlGenerator>();
        });
    }
}