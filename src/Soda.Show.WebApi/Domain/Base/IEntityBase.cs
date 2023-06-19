namespace Soda.Show.WebApi.Base;

public interface IEntityBase
{
    public Guid Id { get; set; }
}

public abstract class EntityBase : IEntityBase
{
    public Guid Id { get; set; } = Guid.NewGuid();
}
