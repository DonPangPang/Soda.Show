namespace Soda.Show.WebApi.Base;

public interface IEntityBase
{
    public Guid Id { get; set; }
}

public class EntityBase : IEntityBase
{
    public Guid Id { get; set; } = Guid.NewGuid();
}
