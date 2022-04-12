using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OGWeb.Core.Commands.Sliders;
using OGWeb.Core.Dtos;
using OGWeb.Core.Queries.Sliders;

namespace OGWeb.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SlidersController : CustomControllerBase
{
    private readonly IMediator _mediator;
    public SlidersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<SliderDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetAll()
    {
        return CreateActionResult(await _mediator.Send(new GetAllSliderQuery()));
    }

    [HttpGet("{sliderId}")]
    [ProducesResponseType(typeof(SliderDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Get(Guid sliderId)
    {
        return CreateActionResult(await _mediator.Send(new GetSliderByIdQuery() { Id = sliderId }));
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Create([FromForm] CreateSliderCommand request)
    {
        return CreateActionResult(await _mediator.Send(request));
    }

    [HttpPut]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Update([FromForm] UpdateSliderCommand request)
    {
        return CreateActionResult(await _mediator.Send(request));
    }

    [HttpDelete]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Delete(Guid sliderId)
    {
        return CreateActionResult(await _mediator.Send(new DeleteSliderCommand() { Id = sliderId }));
    }
}