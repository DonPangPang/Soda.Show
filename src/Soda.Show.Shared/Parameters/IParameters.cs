namespace Soda.Show.Shared;

public interface IParameters
{

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