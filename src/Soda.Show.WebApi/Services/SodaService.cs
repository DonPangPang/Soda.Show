using Microsoft.EntityFrameworkCore;
using Soda.AutoMapper;
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

    Task<bool> UpdateAsync(TViewModel dto);

    Task<bool> UpdateAsync(IEnumerable<TViewModel> dtos);

    Task<bool> DeleteAsync(TViewModel dto);

    Task<bool> DeleteAsync(IEnumerable<TViewModel> dtos);

    Task<bool> DeleteAsync(Guid id);

    Task<bool> DeleteAsync(IEnumerable<Guid> ids);

    Task<bool> AddAsync(TViewModel dto);

    Task<bool> AddAsync(IEnumerable<TViewModel> dtos);
}

public class SodaService<TEntity, TViewModel> : ISodaService<TEntity, TViewModel> where TEntity : EntityBase where TViewModel : class, IViewModel
{
    private readonly ISodaRepository _sodaRepository;

    public SodaService(ISodaRepository sodaRepository)
    {
        _sodaRepository = sodaRepository;
    }

    public async Task<bool> AddAsync(TViewModel dto)
    {
        if (dto is null) throw new ArgumentNullException(nameof(dto));

        var entity = dto.MapTo<TEntity>();

        _sodaRepository.Db.Add(entity);
        return await _sodaRepository.SaveAsync();
    }

    public async Task<bool> AddAsync(IEnumerable<TViewModel> dtos)
    {
        if (dtos is null || !dtos.Any()) throw new ArgumentNullException(nameof(dtos));

        var entities = dtos.MapTo<TEntity>();

        await _sodaRepository.Db.AddRangeAsync(entities);

        return await _sodaRepository.SaveAsync();
    }

    public async Task<bool> DeleteAsync(TViewModel dto)
    {
        if (dto is null) throw new ArgumentNullException(nameof(dto));

        var entity = dto.MapTo<TEntity>();

        _sodaRepository.Db.Remove(entity);
        return await _sodaRepository.SaveAsync();
    }

    public async Task<bool> DeleteAsync(IEnumerable<TViewModel> dtos)
    {
        if (dtos is null || !dtos.Any()) throw new ArgumentNullException(nameof(dtos));

        var entities = dtos.MapTo<TEntity>();

        _sodaRepository.Db.RemoveRange(entities);
        return await _sodaRepository.SaveAsync();
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await _sodaRepository.Query<TEntity>().FirstOrDefaultAsync(x => x.Id == id);

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

    public async Task<bool> UpdateAsync(TViewModel dto)
    {
        if (dto is null)
        {
            throw new ArgumentNullException(nameof(dto));
        }

        var old = await GetAsync(dto.Id);
        if (old is null)
        {
            _sodaRepository.Db.Add(dto);
        }
        else
        {
            old.Map(dto);

            _sodaRepository.Db.Update(dto);
        }

        return await _sodaRepository.SaveAsync();
    }

    public async Task<bool> UpdateAsync(IEnumerable<TViewModel> dtos)
    {
        if (dtos is null || !dtos.Any())
        {
            throw new ArgumentNullException(nameof(dtos));
        }
        var entity = dtos.MapTo<TEntity>();

        _sodaRepository.Db.UpdateRange(entity);
        return await _sodaRepository.SaveAsync();
    }
}