using Soda.Show.Shared.Parameters;
using Soda.Show.Shared.ViewModels;
using Soda.Show.WebApi.Domain;
using Soda.Show.WebApi.Services;

namespace Soda.Show.WebApi.Controllers;

/// <summary>
/// 文件资源
/// </summary>
public class FileResourceController : ApiControllerBase<FileResource, VFileResource, BlogParameters>
{
    private readonly ISodaService<FileResource, VFileResource> _sodaService;

    public FileResourceController(ISodaService<FileResource, VFileResource> sodaService) : base(sodaService)
    {
        _sodaService = sodaService;
    }
}