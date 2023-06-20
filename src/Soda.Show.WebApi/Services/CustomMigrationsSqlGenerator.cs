using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Soda.Show.WebApi.Services
{
    public class CustomMigrationsSqlGenerator : MigrationsSqlGenerator
    {
        public CustomMigrationsSqlGenerator(MigrationsSqlGeneratorDependencies dependencies) : base(dependencies)
        {
        }

        protected override void Generate(Microsoft.EntityFrameworkCore.Migrations.Operations.CreateTableOperation operation, IModel? model, MigrationCommandListBuilder builder, bool terminate = true)
        {
            operation.ForeignKeys.Clear();
            base.Generate(operation, model, builder, terminate);
        }
    }
}