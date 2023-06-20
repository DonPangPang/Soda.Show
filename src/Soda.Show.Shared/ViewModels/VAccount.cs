using System.ComponentModel.DataAnnotations;

namespace Soda.Show.Shared.ViewModels;

public interface IViewModel
{
    public Guid Id { get; set; }
}

public class VAccount : IViewModel
{
    public Guid Id { get; set; }
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
}

public class VUser : IViewModel
{
    public Guid Id { get; set; }
}

public class VVersionRecord : IViewModel
{
    public Guid Id { get; set; }
}

public class VTag : IViewModel
{
    public Guid Id { get; set; }
}

public class VGroup : IViewModel
{
    public Guid Id { get; set; }
}