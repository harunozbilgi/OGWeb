using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OGWeb.Core.Commands.OverViews;
using OGWeb.Core.Dtos;
using OGWeb.Core.Queries.OverViews;

namespace OGWeb.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OverViewsController : CustomControllerBase
{
    private readonly IMediator _mediator;
    public OverViewsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<OverViewDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetAll()
    {
        return CreateActionResult(await _mediator.Send(new GetAllOverViewQuery()));
    }

    [HttpGet("{overViewId}")]
    [ProducesResponseType(typeof(OverViewDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Get(Guid overViewId)
    {
        return CreateActionResult(await _mediator.Send(new GetOverViewByIdQuery() { Id = overViewId }));
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Create([FromForm]CreateOverViewCommand request)
    {
        return CreateActionResult(await _mediator.Send(request));
    }

    [HttpPut]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Update([FromForm]UpdateOverViewCommand request)
    {
        return CreateActionResult(await _mediator.Send(request));
    }

    [HttpDelete]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Delete(Guid overViewId)
    {
        return CreateActionResult(await _mediator.Send(new DeleteOverViewCommand() { Id = overViewId }));
    }
}