namespace Soda.Show.WebApi.Base;

public interface ICreator
{
    public Guid CreatorId { get; set; }
    public DateTime CreateTime { get; set; }
}
