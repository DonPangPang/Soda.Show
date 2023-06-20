using System.ComponentModel.DataAnnotations;

namespace Soda.Show.WebApi.Domain.Base;

// public interface EntityBase { [Key] public Guid Id { get; set; } }

public abstract class EntityBase //: EntityBase
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
}