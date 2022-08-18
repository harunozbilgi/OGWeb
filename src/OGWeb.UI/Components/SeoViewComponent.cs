using MediatR;
using Microsoft.AspNetCore.Mvc;
using OGWeb.Core.Queries.AppSeos;

namespace OGWeb.UI.Components;

public class SeoViewComponent: ViewComponent
{
    private readonly IMediator _mediator;

    public SeoViewComponent(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<IViewComponentResult> InvokeAsync(string page = null!)
    {
        var response = await _mediator.Send(new GetAppSeoByPageQuery() { Page = page });

        return View(response.Data);
    }
}
