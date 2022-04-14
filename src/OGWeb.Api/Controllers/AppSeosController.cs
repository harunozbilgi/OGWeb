using MediatR;
using Microsoft.AspNetCore.Mvc;
using OGWeb.Core.Commands.AppSeos;
using OGWeb.Core.Dtos;
using OGWeb.Core.Queries.AppSeos;
using System.Net;

namespace OGWeb.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppSeosController : CustomControllerBase
    {
        private readonly IMediator _mediator;

        public AppSeosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AppSeoDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAll()
        {
            return CreateActionResult(await _mediator.Send(new GetAllAppSeoQuery()));
        }

        [HttpGet("{appSeoId}")]
        [ProducesResponseType(typeof(AppSeoDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Get(Guid appSeoId)
        {
            return CreateActionResult(await _mediator.Send(new GetAppSeoByIdQuery() { Id = appSeoId }));
        }

        [HttpGet]
        [Route("Page/{page}")]
        [ProducesResponseType(typeof(AppSeoDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetPage(string page)
        {
            return CreateActionResult(await _mediator.Send(new GetAppSeoByPageQuery() { Page = page }));
        }


        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Create(CreateAppSeoCommand request)
        {
            return CreateActionResult(await _mediator.Send(request));
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Update(UpdateAppSeoCommand request)
        {
            return CreateActionResult(await _mediator.Send(request));
        }

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete(Guid videoId)
        {
            return CreateActionResult(await _mediator.Send(new DeleteAppSeoCommand() { Id = videoId }));
        }
    }
}
