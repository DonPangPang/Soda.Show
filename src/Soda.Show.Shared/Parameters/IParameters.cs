using System.Reflection;
namespace Soda.Show.Shared;

public interface IParameters
{
    IEnumerable<PropertyInfo> GetProperties();
}

public class Parameters : IParameters
{
    public IEnumerable<PropertyInfo> GetProperties()
    {
        var types = GetType().GetProperties().Where(p =>
            !typeof(IPaging).IsAssignableFrom(p.DeclaringType) &&
            !typeof(ISorting).IsAssignableFrom(p.DeclaringType) &&
            !typeof(IDateRange).IsAssignableFrom(p.DeclaringType));

        return types.Where(x => x.GetValue(this) != null);
    }
}

public interface IPaging
{
    /// <summary>
    /// 页码
    /// </summary>
    public int Page { get; set; }

    /// <summary>
    /// 每页大小
    /// </summary>
    public int PageSize { get; set; }
}

public interface ISorting
{
    /// <summary>
    /// 排序
    /// </summary>
    public string? OrderBy { get; set; }
}

public interface IDateRange
{
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
}