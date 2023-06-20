using Microsoft.AspNetCore.Mvc;
using Soda.Show.Shared.Parameters;
using Soda.Show.Shared.ViewModels;
using Soda.Show.WebApi.Domain.Base;
using Soda.Show.WebApi.Helpers;
using Soda.Show.WebApi.Services;

namespace Soda.Show.WebApi.Controllers;

[ApiController]
[Route("[Controller]")]
public class ApiControllerBase : ControllerBase
{
    [NonAction]
    public ActionResult Success(object? data = null)
    {
        return Ok(new SodaResult
        {
            Data = data
        });
    }

    [NonAction]
    public ActionResult Success(string? message, object? data = null)
    {
        return Ok(new SodaResult
        {
            Message = message,
            Data = data
        });
    }

    [NonAction]
    public ActionResult Fail(object? data = null)
    {
        return BadRequest(new SodaResult
        {
            Data = data
        });
    }

    [NonAction]
    public ActionResult Fail(string? message, object? data = null)
    {
        return BadRequest(new SodaResult
        {
            Message = message,
            Data = data
        });
    }
}

public class ApiControllerBase<TEntity, TViewModel, TParameters> : ApiControllerBase
    where TEntity : EntityBase
    where TViewModel : class, IViewModel
    where TParameters : class, IParameters
{
    private readonly ISodaService<TEntity, TViewModel> _service;

    public ApiControllerBase(ISodaService<TEntity, TViewModel> service)
    {
        _service = service;
    }

    [HttpGet]
    public virtual async Task<IActionResult> GetAsync([FromQuery] TParameters parameters)
    {
        var res = await _service.GetAsync(parameters);

        return Success(res);
    }

    [HttpGet("{id}")]
    public virtual async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var res = await _service.GetAsync(id);

        return Success(res);
    }

    [HttpPut]
    public virtual async Task<IActionResult> UpdateAsync([FromBody] TViewModel entity)
    {
        var res = await _service.UpdateAsync(entity);

        return res ? Success(await _service.GetAsync(entity.Id)) : Fail("更新失败");
    }

    [HttpPost]
    public virtual async Task<IActionResult> AddAsync([FromBody] TViewModel entity)
    {
        var res = await _service.AddAsync(entity);

        return res ? Success() : Fail("添加失败");
    }

    [HttpDelete("{id}")]
    public virtual async Task<IActionResult> DeleteAsync(Guid id)
    {
        var res = await _service.DeleteAsync(id);

        return res ? Success() : Fail("删除失败");
    }
}