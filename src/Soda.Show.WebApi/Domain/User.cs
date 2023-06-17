using Soda.Show.Shared.Enums;
using Soda.Show.WebApi.Base;

namespace Soda.Show.WebApi.Domain;

public class User : EntityBase, ISoftDelete, ICreator, IModifier
{
    public bool Deleted { get; set; }
    public Guid CreatorId { get; set; }
    public User? Creator { get; set; }
    public DateTime CreateTime { get; set; }
    public Guid? ModifierId { get; set; }
    public User? Modifier { get; set; }
    public DateTime? UpdateTime { get; set; }

    public required string Name { get; set; }
    public Gender Gender { get; set; } = Gender.None;
    public string? Description { get; set; }
    public DateTime BornDate { get; set; }
    public string? Address { get; set; }

    public Guid AccountId { get; set; }
    public Account? Account { get; set; }
}
