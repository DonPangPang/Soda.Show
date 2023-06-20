using Soda.Show.Shared;
using Soda.Show.WebApi.Base;
using System.Linq.Dynamic.Core;

namespace Soda.Show.WebApi;

/// <summary>
/// </summary>
public static class IQueryableExtensions
{
    /// <summary>
    /// 进行排序
    /// </summary>
    /// <typeparam name="T"> </typeparam>
    /// <param name="source">  </param>
    /// <param name="orderBy"> </param>
    /// <returns> </returns>
    public static IQueryable<T> ApplySort<T>(
        this IQueryable<T> source, string orderBy) where T : class, IEntityBase
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        if (string.IsNullOrWhiteSpace(orderBy))
        {
            return source;
        }

        var orderByAfterSplit = orderBy.Split(",");

        foreach (var orderByClause in orderByAfterSplit.Reverse())
        {
            var trimedOrderByClause = orderByClause.Trim();

            var orderDescending = trimedOrderByClause.EndsWith(" desc");

            var indexOfFirstSpace = trimedOrderByClause.IndexOf(" ", StringComparison.Ordinal);

            var propertyName = indexOfFirstSpace == -1
                ? trimedOrderByClause
                : trimedOrderByClause.Remove(indexOfFirstSpace);

            source = source.OrderBy(propertyName
                                    + (orderDescending ? " descending" : " ascending"));
        }

        return source;
    }

    /// <summary>
    /// 分页
    /// </summary>
    /// <typeparam name="T"> </typeparam>
    /// <param name="source"> </param>
    /// <param name="paging"> </param>
    /// <returns> </returns>
    public static async Task<PagedList<T>> ToPagedListAsync<T>(this IQueryable<T> source, IPaging paging) where T : class, IEntityBase
    {
        return await PagedList<T>.CreateAsync(source, paging.Page, paging.PageSize);
    }

    /// <summary>
    /// 排序并分页
    /// </summary>
    /// <typeparam name="T"> </typeparam>
    /// <param name="source">     </param>
    /// <param name="parameters"> </param>
    /// <returns> </returns>
    public static async Task<PagedList<T>> QueryAsync<T>(this IQueryable<T> source, IParameters parameters) where T : class, IEntityBase
    {
        if (parameters is ISorting sorting)
            source = source.ApplySort(sorting.OrderBy ?? "");

        if (parameters is IPaging paging && paging.PageSize > 0)
            return await source.ToPagedListAsync(paging);
        else
            return await PagedList<T>.CreateAsync(source, 1, 999);
    }
}