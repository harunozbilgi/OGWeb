using MediatR;
using Microsoft.AspNetCore.Mvc;
using OGWeb.Core.Queries.AppSettings;

namespace OGWeb.UI.Components;

public class FooterViewComponent : ViewComponent
{
    private readonly IMediator _mediator;

    public FooterViewComponent(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<IViewComponentResult> InvokeAsync(string page = null!)
    {
        var configs = await _mediator.Send(new GetAppSettingByQuery());

        ViewData["page"] = page;

        return View(configs.Data);
    }
}
