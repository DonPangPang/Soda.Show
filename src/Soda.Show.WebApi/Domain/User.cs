using System.ComponentModel.DataAnnotations.Schema;
using Soda.Show.Shared.Enums;
using Soda.Show.WebApi.Domain.Base;

namespace Soda.Show.WebApi.Domain;

public class User : EntityBase, ISoftDelete, ICreator, IModifier
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

    public string Name { get; set; } = string.Empty;
    public Gender Gender { get; set; } = Gender.None;
    public string? Description { get; set; }
    public DateTime BornDate { get; set; }
    public string? Address { get; set; }

    public Guid AccountId { get; set; }

    [NotMapped]
    public Account? Account { get; set; }
}