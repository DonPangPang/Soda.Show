using Soda.Show.Shared.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using System.Security.Principal;

namespace Soda.Show.Shared.ViewModels;

public interface IViewModel
{
    public Guid Id { get; set; }
}

public class VAccount : IViewModel
{
    public Guid Id { get; set; }

    public Guid CreatorId { get; set; }

    //public VUser? Creator { get; set; }

    public DateTime CreateTime { get; set; }
    public Guid? ModifierId { get; set; }

    //public VUser? Modifier { get; set; }

    public DateTime? UpdateTime { get; set; }

    public string Username { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public Guid UserId { get; set; }

    //public VUser? User { get; set; }

    public bool IsSuper { get; set; } = false;
    public bool Enabled { get; set; } = true;
}

public class VBlog : IViewModel
{
    public Guid Id { get; set; }

    public Guid CreatorId { get; set; }
    public VUser? Creator { get; set; }
    public DateTime CreateTime { get; set; }
    public Guid? ModifierId { get; set; }
    public VUser? Modifier { get; set; }
    public DateTime? UpdateTime { get; set; }

    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Description { get; set; }

    public string Content { get; set; } = string.Empty;

    public ICollection<VTag>? Tags { get; set; } = new List<VTag>();
    public ICollection<VGroup>? Groups { get; set; } = new List<VGroup>();
    public ICollection<VFileResource>? FileInfos { get; set; } = new List<VFileResource>();
}

public class VFileResource : IViewModel
{
    public Guid Id { get; set; }

    public Guid CreatorId { get; set; }

    //public VUser? Creator { get; set; }

    public DateTime CreateTime { get; set; }

    public string Path { get; set; } = string.Empty;
    public long Size { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Suffix { get; set; } = string.Empty;

    public Guid BlogId { get; set; }

    //public VBlog? Blog { get; set; }
}

public class VUser : IViewModel
{
    public Guid Id { get; set; }

    public Guid CreatorId { get; set; }

    public VUser? Creator { get; set; }

    public DateTime CreateTime { get; set; }
    public Guid? ModifierId { get; set; }

    public VUser? Modifier { get; set; }

    public DateTime? UpdateTime { get; set; }

    public string Name { get; set; } = string.Empty;
    public Gender Gender { get; set; } = Gender.None;
    public string? Description { get; set; }
    public DateTime BornDate { get; set; }
    public string? Address { get; set; }
    public Guid AccountId { get; set; }

    //public VAccount? Account { get; set; }
}

public class VVersionRecord : IViewModel
{
    public Guid Id { get; set; }
    public Guid CreatorId { get; set; }

    //public VUser? Creator { get; set; }

    public DateTime CreateTime { get; set; }
    public Guid? ModifierId { get; set; }
    public DateTime? UpdateTime { get; set; }

    public Guid RecordId { get; set; }
    public string Domain { get; set; } = string.Empty;
    public string Data { get; set; } = string.Empty;
}

public class VTag : IViewModel
{
    public Guid Id { get; set; }

    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    //public ICollection<VBlog>? Blogs { get; set; }
}

public class VGroup : IViewModel
{
    public Guid Id { get; set; }

    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    //public ICollection<VBlog>? Blogs { get; set; }
}