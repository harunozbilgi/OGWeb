using MediatR;
using Microsoft.AspNetCore.Mvc;
using OGWeb.Core.Commands.AppSeos;
using OGWeb.Core.Queries.AppSeos;

namespace OGWeb.UI.Areas.Cms.Controllers;

[Area("Cms")]
public class AppSeosController : Controller
{
    private readonly IMediator _mediator;

    public AppSeosController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IActionResult> Index()
    {
        var response = await _mediator.Send(new GetAllAppSeoQuery());

        return View(response.Data.ToList());
    }

    #region Create
    public IActionResult Create()
    {
        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateAppSeoCommand createAppSeo)
    {
        var work = await _mediator.Send(createAppSeo);

        return RedirectToAction(nameof(Index));
    }

    #endregion

    #region Edit
    public async Task<IActionResult> Edit(Guid id)
    {
        var response = await _mediator.Send(new GetAppSeoByIdQuery() { Id = id });
        if (response.Data == null)
            return NotFound();

        var overView = response.Data;

        UpdateAppSeoCommand updateAppSeo = new()
        {
            Id = response.Data.Id,
            Title = overView.Title,
            Description = overView.Description,
            Keyword = overView.Keyword,
            Page = overView.Page,
        };


        return View(updateAppSeo);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(UpdateAppSeoCommand updateAppSeo)
    {
        var work = await _mediator.Send(updateAppSeo);

        return RedirectToAction(nameof(Index));
    }
    #endregion

    #region Delete
    public async Task<IActionResult> Delete(Guid id)
    {
        var response = await _mediator.Send(new DeleteAppSeoCommand() { Id = id });
        return RedirectToAction(nameof(Index));
    }
    #endregion
}
