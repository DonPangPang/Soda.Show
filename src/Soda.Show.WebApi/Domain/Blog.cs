using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Soda.Show.WebApi.Domain.Base;

namespace Soda.Show.WebApi.Domain;

public class Blog : EntityBase, ISoftDelete, ICreator, IModifier
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

    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    public string? _description;

    [MaxLength(500)]
    [BackingField(nameof(_description))]
    public string? Description
    {
        get => _description;

        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                _description = Content[..200];
            }

            _description = value;
        }
    }

    public string Content { get; set; } = string.Empty;

    public ICollection<Tag>? Tags { get; set; } = new List<Tag>();
    public ICollection<Group>? Groups { get; set; } = new List<Group>();
    public ICollection<FileResource>? FileInfos { get; set; } = new List<FileResource>();
}

public class Tag : EntityBase
{
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    public ICollection<Blog>? Blogs { get; set; }
}

public class Group : EntityBase
{
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    public ICollection<Blog>? Blogs { get; set; }
}