namespace Soda.Show.WebApi.Domain.Base;

public interface ICreator
{
    public Guid CreatorId { get; set; }
    public User? Creator { get; set; }
    public DateTime CreateTime { get; set; }
}