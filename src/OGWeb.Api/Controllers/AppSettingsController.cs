using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OGWeb.Core.Commands.AppSettings;
using OGWeb.Core.Dtos;
using OGWeb.Core.Queries.AppSettings;

namespace OGWeb.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppSettingsController : CustomControllerBase
{
    private readonly IMediator _mediator;

    public AppSettingsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(AppSettingDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Get()
    {
        return CreateActionResult(await _mediator.Send(new GetAppSettingByQuery()));
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Create([FromForm] AppSettingCommand request)
    {
        return CreateActionResult(await _mediator.Send(request));
    }
}