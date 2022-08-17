using MediatR;
using Microsoft.AspNetCore.Mvc;
using OGWeb.Core.Commands.Sliders;
using OGWeb.Core.Commands.Works;
using OGWeb.Core.Queries.Sliders;
using OGWeb.Core.Queries.Works;

namespace OGWeb.UI.Areas.Cms.Controllers;

[Area("Cms")]
public class SlidersController : Controller
{
    private readonly IMediator _mediator;

    public SlidersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IActionResult> Index()
    {
        var response = await _mediator.Send(new GetAllSliderQuery());


        return View(response.Data.ToList());
    }

    #region Create
    public IActionResult Create()
    {
        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateSliderCommand createSlider)
    {
        var work = await _mediator.Send(createSlider);

        return RedirectToAction(nameof(Index));
    }

    #endregion

    #region Edit
    public async Task<IActionResult> Edit(Guid id)
    {
        var response = await _mediator.Send(new GetSliderByIdQuery() { Id = id });
        if (response.Data == null)
            return NotFound();

        var slider = response.Data;

        var UpdateSliderCommand = new UpdateSliderCommand()
        {
            Id = response.Data.Id,
            Title = slider.Title,
        };

        ViewData["ImageUrl"] = slider.ImageUrl;

        return View(UpdateSliderCommand);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(UpdateSliderCommand updateSlider)
    {
        var work = await _mediator.Send(updateSlider);

        return RedirectToAction(nameof(Index));
    }
    #endregion

    #region Delete
    public async Task<IActionResult> Delete(Guid id)
    {
        var response = await _mediator.Send(new DeleteSliderCommand() { Id = id });
        return RedirectToAction(nameof(Index));
    }
    #endregion
}
