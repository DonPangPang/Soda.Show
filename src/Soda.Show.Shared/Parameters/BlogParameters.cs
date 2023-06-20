using System.ComponentModel.DataAnnotations;
using Soda.Show.WebApi;

namespace Soda.Show.Shared;

public class BlogParameters : Parameters, IDateRange, IPaging, ISorting
{
    [CompareFunc(Operation.Contains)]
    public string? Title { get; set; }

    [CompareFunc(Operation.Contains, "Tags.Name")]
    public string? Tag { get; set; }

    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public string? OrderBy { get; set; }
}
