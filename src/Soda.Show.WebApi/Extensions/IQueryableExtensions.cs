using Soda.Show.Shared;
using Soda.Show.WebApi.Base;
using Soda.Show.WebApi.Helpers;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;

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
        this IQueryable<T> source, string orderBy) where T : class
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
            var trimOrderByClause = orderByClause.Trim();

            var orderDescending = trimOrderByClause.EndsWith(" desc");

            var indexOfFirstSpace = trimOrderByClause.IndexOf(" ", StringComparison.Ordinal);

            var propertyName = indexOfFirstSpace == -1
                ? trimOrderByClause
                : trimOrderByClause.Remove(indexOfFirstSpace);

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
    public static async Task<PagedList<T>> ToPagedListAsync<T>(this IQueryable<T> source, IPaging paging) where T : class
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
    public static async Task<PagedList<T>> QueryAsync<T>(this IQueryable<T> source, IParameters parameters) where T : class
    {
        if (parameters is ISorting sorting)
            source = source.ApplySort(sorting.OrderBy ?? "");

        if (parameters is IDateRange date && typeof(T).IsAssignableFrom(typeof(ICreator)))
        {
            source = (IQueryable<T>)(source as IQueryable<ICreator>)!.Where(x => x.CreateTime >= date.StartTime && x.CreateTime <= date.EndTime);
        }

        var props = parameters.GetProperties();
        if (props.Any())
        {
            foreach (var property in props)
            {
                var value = property.GetValue(parameters)!;

                var attr = property.GetCustomAttribute<CompareFuncAttribute>();

                source = attr?.Comparer switch
                {
                    Operation.Contains => source.ContainsOper<T>(attr.PropertyName ?? property.Name, value),
                    Operation.GreaterThan => source.GreaterThanOper<T>(attr.PropertyName ?? property.Name, value),
                    Operation.LessThan => source.LessThanOper<T>(attr.PropertyName ?? property.Name, value),
                    Operation.GreaterThanOrEqual => source.GreaterThanOrEqualOper<T>(attr.PropertyName ?? property.Name, value),
                    Operation.LessThanOrEqual => source.LessThanOrEqualOper<T>(attr.PropertyName ?? property.Name, value),
                    Operation.Equal or
                    _ => source.WhereOper<T>(attr?.PropertyName ?? property.Name, value)
                };
            }
        }

        if (parameters is IPaging paging && paging.PageSize > 0)
            return await source.ToPagedListAsync(paging);
        else
            return await PagedList<T>.CreateAsync(source, 1, 999);
    }

    public static Expression<Func<T, bool>> GetEqualExpression<T>(string propertyName, object propertyValue)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var property = Expression.Property(parameter, propertyName);

        var constant = Expression.Constant(propertyValue);

        return Expression.Lambda<Func<T, bool>>(Expression.Equal(property, constant), parameter);
    }

    public static Expression<Func<T, bool>> GetContainsExpression<T>(string propertyName, object propertyValue)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var property = Expression.Property(parameter, propertyName);
        var constant = Expression.Constant($"%{propertyValue}%");
        var method = typeof(string).GetMethod("Contains", new[] { typeof(string) })!;
        var call = Expression.Call(property, method, constant);

        return Expression.Lambda<Func<T, bool>>(call, parameter);
    }

    public static IQueryable<T> WhereOper<T>(this IQueryable<T> source, string propertyName, object propertyValue)
    {
        return source.Where($"{propertyName} == @0", propertyValue);
    }
    public static IQueryable<T> ContainsOper<T>(this IQueryable<T> source, string propertyName, object propertyValue)
    {
        return source.Where($"{propertyName}.Contains(@0)", propertyValue);
    }
    public static IQueryable<T> GreaterThanOper<T>(this IQueryable<T> source, string propertyName, object propertyValue)
    {
        return source.Where($"{propertyName} > @0", propertyValue);
    }
    public static IQueryable<T> LessThanOper<T>(this IQueryable<T> source, string propertyName, object propertyValue)
    {
        return source.Where($"{propertyName} < @0", propertyValue);
    }
    public static IQueryable<T> GreaterThanOrEqualOper<T>(this IQueryable<T> source, string propertyName, object propertyValue)
    {
        return source.Where($"{propertyName} >= @0", propertyValue);
    }
    public static IQueryable<T> LessThanOrEqualOper<T>(this IQueryable<T> source, string propertyName, object propertyValue)
    {
        return source.Where($"{propertyName} <= @0", propertyValue);
    }

    public static Expression<Func<T, bool>> GetGreaterThanExpression<T>(string propertyName, object propertyValue)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var property = Expression.Property(parameter, propertyName);

        var constant = Expression.Constant(propertyValue);

        return Expression.Lambda<Func<T, bool>>(Expression.GreaterThan(property, constant), parameter);
    }

    public static Expression<Func<T, bool>> GetLessThanExpression<T>(string propertyName, object propertyValue)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var property = Expression.Property(parameter, propertyName);

        var constant = Expression.Constant(propertyValue);

        return Expression.Lambda<Func<T, bool>>(Expression.LessThan(property, constant), parameter);
    }

    public static Expression<Func<T, bool>> GetGreaterThanOrEqualExpression<T>(string propertyName, object propertyValue)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var property = Expression.Property(parameter, propertyName);

        var constant = Expression.Constant(propertyValue);

        return Expression.Lambda<Func<T, bool>>(Expression.GreaterThanOrEqual(property, constant), parameter);
    }

    public static Expression<Func<T, bool>> GetLessThanOrEqualExpression<T>(string propertyName, object propertyValue)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var property = Expression.Property(parameter, propertyName);

        var constant = Expression.Constant(propertyValue);

        return Expression.Lambda<Func<T, bool>>(Expression.LessThanOrEqual(property, constant), parameter);
    }
}