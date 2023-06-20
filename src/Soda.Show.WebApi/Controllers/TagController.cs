using Soda.Show.Shared.Parameters;
using Soda.Show.Shared.ViewModels;
using Soda.Show.WebApi.Domain;
using Soda.Show.WebApi.Services;

namespace Soda.Show.WebApi.Controllers;

/// <summary>
/// ±Í«©
/// </summary>
public class TagController : ApiControllerBase<Tag, VTag, BlogParameters>
{
    private readonly ISodaService<Tag, VTag> _sodaService;

    public TagController(ISodaService<Tag, VTag> sodaService) : base(sodaService)
    {
        _sodaService = sodaService;
    }
}