using Microsoft.EntityFrameworkCore;
using Soda.Show.WebApi.Domain;
using Soda.Show.WebApi.Domain.Base;
using System.Reflection;

namespace Soda.Show.WebApi.Data
{
    public class SodaDbContext : DbContext
    {
        public SodaDbContext(DbContextOptions<SodaDbContext> options) : base(options)
        {
        }

        private static readonly MethodInfo ConfigureBasePropertiesMethodInfo = typeof(SodaDbContext)
                .GetMethod(
                    nameof(ConfigGlobalFilter),
                    BindingFlags.Instance | BindingFlags.NonPublic
                )!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var type in typeof(EntityBase).Assembly.GetTypes().Where(t => !t.IsAbstract && !t.IsInterface && t.IsSubclassOf(typeof(EntityBase))))
            {
                if (builder.Model.FindEntityType(type) == null)
                {
                    builder.Model.AddEntityType(type);

                    if (type.IsAssignableFrom(typeof(ISoftDelete)))
                    {
                        ConfigureBasePropertiesMethodInfo.MakeGenericMethod(type).Invoke(null, new object[] { builder });
                    }
                }
            }

            builder.Entity<Account>().HasData(new Account
            {
                Username = "admin",
                Password = "123456",
                User = new User
                {
                    Name = "admin",
                },
                IsSuper = true
            });

            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            try
            {
                AutoSetChangedEntities();
                return base.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw;
            }
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                AutoSetChangedEntities();
                return await base.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException)
            {
                throw;
            }
        }

        private void AutoSetChangedEntities()
        {
            foreach (var dbEntityEntry in ChangeTracker.Entries<EntityBase>())
            {
                var baseEntity = dbEntityEntry.Entity;
                switch (dbEntityEntry.State)
                {
                    case EntityState.Added:
                        if (baseEntity is ICreator creatorEntity)
                        {
                            creatorEntity.CreateTime = DateTime.Now;
                        }
                        break;

                    case EntityState.Modified:
                        if (baseEntity is IModifier modifiedEntity)
                        {
                            modifiedEntity.UpdateTime = DateTime.Now;
                        }
                        break;

                    case EntityState.Deleted:
                        if (baseEntity is ISoftDelete deletedEntity)
                        {
                            deletedEntity.Deleted = true;
                            dbEntityEntry.State = EntityState.Modified;
                        }
                        break;
                }
            }
        }

        private static void ConfigGlobalFilter<TEntity>(ModelBuilder builder) where TEntity : class, ISoftDelete
        {
            builder.Entity<TEntity>().HasQueryFilter(x => !x.Deleted);
        }
    }
}