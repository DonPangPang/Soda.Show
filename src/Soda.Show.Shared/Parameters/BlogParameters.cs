using Soda.Show.Shared.Attributes;

namespace Soda.Show.Shared.Parameters;

public class BlogParameters : Parameters, IDateRange, IPaging, ISorting
{
    [CompareFunc(Operation.Contains)]
    public string? Title { get; set; }

    [CompareFunc(Operation.Contains, "Tags.Name")]
    public string? Tag { get; set; }

    [CompareFunc(Operation.Contains, "Groups.Name")]
    public string? Group { get; set; }

    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public string? OrderBy { get; set; }
}

public class AccountParameters : Parameters, IDateRange, IPaging, ISorting
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public string? OrderBy { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
}

public class UserParameters : Parameters, IDateRange, IPaging, ISorting
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public string? OrderBy { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
}

public class FileResourceParameters : Parameters, IDateRange, IPaging, ISorting
{
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public string? OrderBy { get; set; }
}

public class VersionRecordParameters : Parameters, IDateRange, IPaging, ISorting
{
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public string? OrderBy { get; set; }
}

public class TagParameters : Parameters, IPaging, ISorting
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public string? OrderBy { get; set; }
}

public class GroupParameters : Parameters, IPaging, ISorting
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public string? OrderBy { get; set; }
}