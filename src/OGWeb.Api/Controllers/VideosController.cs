using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OGWeb.Core.Commands.Videos;
using OGWeb.Core.Dtos;
using OGWeb.Core.Queries.Videos;

namespace OGWeb.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VideosController : CustomControllerBase
{
    private readonly IMediator _mediator;
    public VideosController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<VideoDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetAll()
    {
        return CreateActionResult(await _mediator.Send(new GetAllVideoQuery()));
    }

    [HttpGet("{videoId}")]
    [ProducesResponseType(typeof(WorkDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Get([FromQuery]Guid videoId)
    {
        return CreateActionResult(await _mediator.Send(new GetVideoByIdQuery() { Id = videoId }));
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Create([FromBody]CreateVideoCommand request)
    {
        return CreateActionResult(await _mediator.Send(request));
    }

    [HttpPut]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Update(UpdateVideoCommand request)
    {
        return CreateActionResult(await _mediator.Send(request));
    }

    [HttpDelete]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Delete(Guid videoId)
    {
        return CreateActionResult(await _mediator.Send(new DeleteVideoCommand() { Id = videoId }));
    }
}