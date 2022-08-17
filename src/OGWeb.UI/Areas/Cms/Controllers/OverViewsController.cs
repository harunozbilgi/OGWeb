using MediatR;
using Microsoft.AspNetCore.Mvc;
using OGWeb.Core.Commands.OverViews;
using OGWeb.Core.Queries.OverViews;

namespace OGWeb.UI.Areas.Cms.Controllers;

[Area("Cms")]
public class OverViewsController : Controller
{
    private readonly IMediator _mediator;

    public OverViewsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IActionResult> Index()
    {
        var response = await _mediator.Send(new GetAllOverViewQuery());
        return View(response.Data.ToList());
    }


    #region Create
    public IActionResult Create()
    {
        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateOverViewCommand createOverView)
    {
        var work = await _mediator.Send(createOverView);

        return RedirectToAction(nameof(Index));
    }

    #endregion

    #region Edit
    public async Task<IActionResult> Edit(Guid id)
    {
        var response = await _mediator.Send(new GetOverViewByIdQuery() { Id = id });
        if (response.Data == null)
            return NotFound();

        var overView = response.Data;

        UpdateOverViewCommand updateOverView = new()
        {
            Id = response.Data.Id,
            Title = overView.Title,
            Description = overView.Description,
            ImageUrl_One = overView.ImageUrl_One,
            ImageUrl_Two = overView.ImageUrl_Two,
        };


        return View(updateOverView);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(UpdateOverViewCommand updateOverView)
    {
        var work = await _mediator.Send(updateOverView);

        return RedirectToAction(nameof(Index));
    }
    #endregion

    #region Delete
    public async Task<IActionResult> Delete(Guid id)
    {
        var response = await _mediator.Send(new DeleteOverViewCommand() { Id = id });
        return RedirectToAction(nameof(Index));
    }
    #endregion

}
