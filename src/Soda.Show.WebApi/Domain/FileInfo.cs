using Soda.Show.WebApi.Base;

namespace Soda.Show.WebApi.Domain;

public class FileInfo : EntityBase, IDelete, ICreator
{
    public bool Deleted { get; set; }
    public Guid CreatorId { get; set; }
    public DateTime CreateTime { get; set; }

    public required string Path { get; set; }
    public required long Size { get; set; }
    public required string Name { get; set; }
    public required string Suffix { get; set; }

    public Guid BlogId { get; set; }
    public Blog? Blog { get; set; }
}
