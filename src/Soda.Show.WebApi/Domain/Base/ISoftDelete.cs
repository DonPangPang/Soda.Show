namespace Soda.Show.WebApi.Domain.Base;

public interface ISoftDelete
{
    public bool Deleted { get; set; }
}