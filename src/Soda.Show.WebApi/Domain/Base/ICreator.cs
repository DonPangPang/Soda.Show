using Soda.Show.WebApi.Domain;

namespace Soda.Show.WebApi.Base;

public interface ICreator
{
    public Guid CreatorId { get; set; }
    public User? Creator { get; set; }
    public DateTime CreateTime { get; set; }
}
