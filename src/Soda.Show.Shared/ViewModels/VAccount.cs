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
}

public class VFileInfo : IViewModel
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