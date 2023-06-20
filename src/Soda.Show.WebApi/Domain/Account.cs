using Soda.Show.WebApi.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Soda.Show.WebApi.Domain;

public class Account : EntityBase, ISoftDelete, ICreator, IModifier, IEnabled
{
    public bool Deleted { get; set; }
    public Guid CreatorId { get; set; }

    [NotMapped]
    public User? Creator { get; set; }

    public DateTime CreateTime { get; set; }
    public Guid? ModifierId { get; set; }

    [NotMapped]
    public User? Modifier { get; set; }

    public DateTime? UpdateTime { get; set; }

    public string Username { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public Guid UserId { get; set; }

    [NotMapped]
    public User? User { get; set; }

    public bool IsSuper { get; set; } = false;
    public bool Enabled { get; set; } = true;
}