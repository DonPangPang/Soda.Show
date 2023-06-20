using Soda.Show.Shared.Parameters;
using Soda.Show.Shared.ViewModels;
using Soda.Show.WebApi.Domain;
using Soda.Show.WebApi.Services;

namespace Soda.Show.WebApi.Controllers;

/// <summary>
/// °æ±¾¼ÇÂ¼
/// </summary>
public class VersionRecordController : ApiControllerBase<VersionRecord, VVersionRecord, BlogParameters>
{
    private readonly ISodaService<VersionRecord, VVersionRecord> _sodaService;

    public VersionRecordController(ISodaService<VersionRecord, VVersionRecord> sodaService) : base(sodaService)
    {
        _sodaService = sodaService;
    }
}