using Soda.Show.WebApi.Base;

namespace Soda.Show.WebApi.Domain;

public class Account : EntityBase, ISoftDelete, ICreator, IModifier
{
    public bool Deleted { get; set; }
    public Guid CreatorId { get; set; }
    public User? Creator { get; set; }

    public DateTime CreateTime { get; set; }
    public Guid? ModifierId { get; set; }
    public User? Modifier { get; set; }
    public DateTime? UpdateTime { get; set; }

    public required string Username { get; set; }

    public required string Password { get; set; }

    public Guid UserId { get; set; }
    public User? User { get; set; }

    public bool IsSuper { get; set; } = false;
}