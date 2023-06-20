using Soda.Show.Shared.Parameters;
using Soda.Show.Shared.ViewModels;
using Soda.Show.WebApi.Domain;
using Soda.Show.WebApi.Services;

namespace Soda.Show.WebApi.Controllers;

/// <summary>
/// ·Ö×é
/// </summary>
public class GroupController : ApiControllerBase<Group, VGroup, BlogParameters>
{
    private readonly ISodaService<Group, VGroup> _sodaService;

    public GroupController(ISodaService<Group, VGroup> sodaService) : base(sodaService)
    {
        _sodaService = sodaService;
    }
}