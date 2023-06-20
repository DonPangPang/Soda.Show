using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Soda.Show.WebApi.Base;

// public interface EntityBase
// {
//     [Key]
//     public Guid Id { get; set; }
// }

public abstract class EntityBase //: EntityBase
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
}
