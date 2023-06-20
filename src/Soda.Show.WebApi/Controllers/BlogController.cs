using Soda.Show.Shared;
using Soda.Show.Shared.ViewModels;
using Soda.Show.WebApi.Domain;

namespace Soda.Show.WebApi.Controllers;

/// <summary>
/// ²©¿Í
/// </summary>
public class BlogController : ApiControllerBase<Blog, VBlog, BlogParameters>
{
    private readonly ISodaService<Blog, VBlog> _sodaService;

    public BlogController(ISodaService<Blog, VBlog> sodaService) : base(sodaService)
    {
        _sodaService = sodaService;
    }
}