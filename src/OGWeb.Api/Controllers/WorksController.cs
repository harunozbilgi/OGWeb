using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OGWeb.Core.Commands.Works;
using OGWeb.Core.Dtos;
using OGWeb.Core.Queries.Works;

namespace OGWeb.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorksController : CustomControllerBase
{
    private readonly IMediator _mediator;
    public WorksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<WorkDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetAll()
    {
        return CreateActionResult(await _mediator.Send(new GetAllWorkQuery()));
    }

    [HttpGet("{workId}")]
    [ProducesResponseType(typeof(WorkDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Get(Guid workId)
    {
        return CreateActionResult(await _mediator.Send(new GetWorkByIdQuery() { Id = workId }));
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Create(CreateWorkCommand request)
    {
        return CreateActionResult(await _mediator.Send(request));
    }

    [HttpPut]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Update(UpdateWorkCommand request)
    {
        return CreateActionResult(await _mediator.Send(request));
    }

    [HttpDelete]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Delete(Guid workId)
    {
        return CreateActionResult(await _mediator.Send(new DeleteWorkCommand() { Id = workId }));
    }
}