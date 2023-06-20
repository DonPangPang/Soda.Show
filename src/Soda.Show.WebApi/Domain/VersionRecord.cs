using Soda.Show.WebApi.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Soda.Show.WebApi.Domain;

public class VersionRecord : EntityBase, ISoftDelete, ICreator
{
    public bool Deleted { get; set; }
    public Guid CreatorId { get; set; }

    [NotMapped]
    public User? Creator { get; set; }

    public DateTime CreateTime { get; set; }
    public Guid? ModifierId { get; set; }
    public DateTime? UpdateTime { get; set; }

    public Guid RecordId { get; set; }
    public string Domain { get; set; } = string.Empty;
    public string Data { get; set; } = string.Empty;
}