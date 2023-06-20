using System.ComponentModel.DataAnnotations.Schema;
using Soda.Show.WebApi.Domain.Base;

namespace Soda.Show.WebApi.Domain;

public class FileResource : EntityBase, IDelete, ICreator
{
    public bool Deleted { get; set; }
    public Guid CreatorId { get; set; }

    [NotMapped]
    public User? Creator { get; set; }

    public DateTime CreateTime { get; set; }

    public string Path { get; set; } = string.Empty;
    public long Size { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Suffix { get; set; } = string.Empty;

    public Guid BlogId { get; set; }

    [NotMapped]
    public Blog? Blog { get; set; }
}