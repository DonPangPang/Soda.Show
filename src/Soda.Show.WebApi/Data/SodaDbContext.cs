using Microsoft.EntityFrameworkCore;
using Soda.Show.WebApi.Domain;

namespace Soda.Show.WebApi.Data
{
    public class SodaDbContext : DbContext
    {
        public SodaDbContext(DbContextOptions<SodaDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
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
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }


        private void AttachSoftDeleted()
        {

        }
    }
}