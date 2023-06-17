using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Soda.Show.WebApi.Base;

namespace Soda.Show.WebApi.Domain;

public class Blog : EntityBase, ISoftDelete, ICreator, IModifier
{
    public bool Deleted { get; set; }
    public Guid CreatorId { get; set; }
    public DateTime CreateTime { get; set; }
    public Guid? ModifierId { get; set; }
    public DateTime? UpdateTime { get; set; }

    [MaxLength(200)]
    public required string Title { get; set; }

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

    public required User Creator { get; set; }
    public required string Content { get; set; }


    public ICollection<Tag>? Tags { get; set; } = new List<Tag>();
    public ICollection<Group>? Groups { get; set; } = new List<Group>();

    public ICollection<FileInfo>? FileInfos { get; set; } = new List<FileInfo>();
}

public class Tag : EntityBase
{

    [MaxLength(200)]
    public required string Name { get; set; }

    public ICollection<Blog>? Blogs { get; set; }
}

public class Group : EntityBase
{
    [MaxLength(200)]
    public required string Name { get; set; }

    public ICollection<Blog>? Blogs { get; set; }
}