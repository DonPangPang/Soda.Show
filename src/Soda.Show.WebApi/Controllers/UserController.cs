using Soda.Show.Shared.Parameters;
using Soda.Show.Shared.ViewModels;
using Soda.Show.WebApi.Domain;
using Soda.Show.WebApi.Services;

namespace Soda.Show.WebApi.Controllers;

/// <summary>
/// ”√ªß
/// </summary>
public class UserController : ApiControllerBase<User, VUser, BlogParameters>
{
    private readonly ISodaService<User, VUser> _sodaService;

    public UserController(ISodaService<User, VUser> sodaService) : base(sodaService)
    {
        _sodaService = sodaService;
    }
}