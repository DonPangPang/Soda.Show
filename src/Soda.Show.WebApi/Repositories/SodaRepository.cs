using Soda.Show.WebApi.Base;
using Soda.Show.WebApi.Data;

namespace Soda.Show.WebApi;

public interface ISodaRepository
{
    SodaDbContext Db { get; }
    IQueryable<T> Query<T>() where T : EntityBase;
    Task<bool> SaveAsync();
    Task BeginTransAsync();
    Task CommitTransAsync();
    Task RollbackAsync();
}

[Service(ServiceLifetime.Scoped)]
public class SodaRepository : ISodaRepository
{
    private readonly SodaDbContext _dbContext;

    public SodaRepository(SodaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public SodaDbContext Db => _dbContext;

    public IQueryable<T> Query<T>() where T : EntityBase
    {
        return _dbContext.Set<T>() as IQueryable<T>;
    }

    public async Task<bool> SaveAsync()
    {
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task BeginTransAsync()
    {
        await _dbContext.Database.BeginTransactionAsync();
    }

    public async Task CommitTransAsync()
    {
        await _dbContext.Database.CommitTransactionAsync();
    }

    public async Task RollbackAsync()
    {
        await _dbContext.Database.RollbackTransactionAsync();
    }
}
