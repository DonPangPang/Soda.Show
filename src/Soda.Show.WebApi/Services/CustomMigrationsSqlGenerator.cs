using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Design;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

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