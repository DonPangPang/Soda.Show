using Soda.Show.Shared;
using Soda.Show.Shared.Parameters;
using Soda.Show.Shared.ViewModels;
using Soda.Show.WebApi.Domain;
using Soda.Show.WebApi.Services;

namespace Soda.Show.WebApi.Controllers;

/// <summary>
/// ук╨е
/// </summary>
public class AccountController : ApiControllerBase<Account, VAccount, BlogParameters>
{
    private readonly ISodaService<Account, VAccount> _sodaService;

    public AccountController(ISodaService<Account, VAccount> sodaService) : base(sodaService)
    {
        _sodaService = sodaService;
    }
}