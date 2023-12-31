using Soda.Show.Shared.Parameters;
using Soda.Show.Shared.ViewModels;
using Soda.Show.WebApi.Domain;
using Soda.Show.WebApi.Services;

namespace Soda.Show.WebApi.Controllers;

/// <summary>
/// ����
/// </summary>
public class BlogController : ApiControllerBase<Blog, VBlog, BlogParameters>
{
    private readonly ISodaService<Blog, VBlog> _sodaService;

    public BlogController(ISodaService<Blog, VBlog> sodaService) : base(sodaService)
    {
        _sodaService = sodaService;
    }
}