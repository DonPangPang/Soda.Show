using Microsoft.EntityFrameworkCore;
using Soda.AutoMapper;
using Soda.Show.Shared;
using Soda.Show.Shared.Parameters;
using Soda.Show.Shared.ViewModels;
using Soda.Show.WebApi.Domain.Base;
using Soda.Show.WebApi.Extensions;
using Soda.Show.WebApi.Helpers;
using Soda.Show.WebApi.Repositories;

namespace Soda.Show.WebApi.Services;

public interface ISodaService<TEntity, TViewModel> where TEntity : EntityBase where TViewModel : class, IViewModel
{
    Task<PagedList<TViewModel>> GetAsync(IParameters parameters);

    Task<TViewModel?> GetAsync(Guid id);

    Task<IEnumerable<TViewModel>> GetAsync(IEnumerable<Guid> ids);

    Task<bool> UpdateAsync(TEntity entity);

    Task<bool> UpdateAsync(IEnumerable<TEntity> entities);

    Task<bool> DeleteAsync(TEntity entity);

    Task<bool> DeleteAsync(IEnumerable<TEntity> entities);

    Task<bool> DeleteAsync(Guid id);

    Task<bool> DeleteAsync(IEnumerable<Guid> ids);

    Task<bool> AddAsync(TEntity entity);

    Task<bool> AddAsync(IEnumerable<TEntity> entities);
}

public class SodaService<TEntity, TViewModel> : ISodaService<TEntity, TViewModel> where TEntity : EntityBase where TViewModel : class, IViewModel
{
    private readonly ISodaRepository _sodaRepository;

    public SodaService(ISodaRepository sodaRepository)
    {
        _sodaRepository = sodaRepository;
    }

    public async Task<bool> AddAsync(TEntity entity)
    {
        if (entity is null) throw new ArgumentNullException(nameof(entity));

        _sodaRepository.Db.Add(entity);
        return await _sodaRepository.SaveAsync();
    }

    public async Task<bool> AddAsync(IEnumerable<TEntity> entities)
    {
        if (entities is null || !entities.Any()) throw new ArgumentNullException(nameof(entities));

        await _sodaRepository.Db.AddRangeAsync(entities);

        return await _sodaRepository.SaveAsync();
    }

    public async Task<bool> DeleteAsync(TEntity entity)
    {
        if (entity is null) throw new ArgumentNullException(nameof(entity));
        _sodaRepository.Db.Remove(entity);
        return await _sodaRepository.SaveAsync();
    }

    public async Task<bool> DeleteAsync(IEnumerable<TEntity> entities)
    {
        if (entities is null || !entities.Any()) throw new ArgumentNullException(nameof(entities));
        _sodaRepository.Db.RemoveRange(entities);
        return await _sodaRepository.SaveAsync();
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = _sodaRepository.Query<TEntity>().FirstOrDefaultAsync(x => x.Id == id);

        if (entity is null) return true;

        _sodaRepository.Db.Remove(entity);
        return await _sodaRepository.SaveAsync();
    }

    public async Task<bool> DeleteAsync(IEnumerable<Guid> ids)
    {
        var entities = await _sodaRepository.Query<TEntity>().Where(x => ids.Contains(x.Id)).Map<TEntity, TViewModel>().ToListAsync();

        if (entities is null || !entities.Any()) return true;

        _sodaRepository.Db.RemoveRange(entities);
        return await _sodaRepository.SaveAsync();
    }

    public async Task<PagedList<TViewModel>> GetAsync(IParameters parameters)
    {
        return await _sodaRepository.Query<TEntity>().Map<TEntity, TViewModel>().QueryAsync(parameters);
    }

    public async Task<TViewModel?> GetAsync(Guid id)
    {
        return await _sodaRepository.Query<TEntity>().Where(x => x.Id == id).Map<TEntity, TViewModel>().FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<TViewModel>> GetAsync(IEnumerable<Guid> ids)
    {
        if (ids is null)
        {
            throw new ArgumentNullException(nameof(ids));
        }

        return await _sodaRepository.Query<TEntity>().Where(x => ids.Contains(x.Id)).Map<TEntity, TViewModel>().ToListAsync();
    }

    public async Task<bool> UpdateAsync(TEntity entity)
    {
        if (entity is null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        var old = await GetAsync(entity.Id);
        if (old is null)
        {
            _sodaRepository.Db.Add(entity);
        }
        else
        {
            old.Map(entity);

            _sodaRepository.Db.Update(entity);
        }

        return await _sodaRepository.SaveAsync();
    }

    public async Task<bool> UpdateAsync(IEnumerable<TEntity> entities)
    {
        if (entities is null || !entities.Any())
        {
            throw new ArgumentNullException(nameof(entities));
        }

        _sodaRepository.Db.UpdateRange(entities);
        return await _sodaRepository.SaveAsync();
    }
}