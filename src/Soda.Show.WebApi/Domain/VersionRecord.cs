using Soda.Show.WebApi.Base;

namespace Soda.Show.WebApi.Domain;

public class VersionRecord : EntityBase, ISoftDelete, ICreator
{
    public bool Deleted { get; set; }
    public Guid CreatorId { get; set; }
    public DateTime CreateTime { get; set; }
    public Guid? ModifierId { get; set; }
    public DateTime? UpdateTime { get; set; }

    public Guid RecordId { get; set; }
    public required string Domain { get; set; }
    public required string Data { get; set; }
}
