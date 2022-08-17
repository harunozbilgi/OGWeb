using MediatR;
using Microsoft.AspNetCore.Mvc;
using OGWeb.Core.Commands.Sliders;
using OGWeb.Core.Commands.Videos;
using OGWeb.Core.Queries.Sliders;
using OGWeb.Core.Queries.Videos;

namespace OGWeb.UI.Areas.Cms.Controllers;

[Area("Cms")]
public class VideosController : Controller
{
    private readonly IMediator _mediator;

    public VideosController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IActionResult> Index()
    {
        var response = await _mediator.Send(new GetAllVideoQuery());


        return View(response.Data.ToList());
    }

    #region Create
    public IActionResult Create()
    {
        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateVideoCommand createVideo)
    {
        var work = await _mediator.Send(createVideo);

        return RedirectToAction(nameof(Index));
    }

    #endregion

    #region Edit
    public async Task<IActionResult> Edit(Guid id)
    {
        var response = await _mediator.Send(new GetVideoByIdQuery() { Id = id });
        if (response.Data == null)
            return NotFound();

        var slider = response.Data;

        var UpdateSliderCommand = new UpdateVideoCommand()
        {
            Id = response.Data.Id,
            Title = slider.Title,
            Url = slider.Url,
            ImageUrl= slider.ImageUrl,
        };

        return View(UpdateSliderCommand);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(UpdateVideoCommand updateVideo)
    {
        var work = await _mediator.Send(updateVideo);

        return RedirectToAction(nameof(Index));
    }
    #endregion

    #region Delete
    public async Task<IActionResult> Delete(Guid id)
    {
        var response = await _mediator.Send(new DeleteVideoCommand() { Id = id });
        return RedirectToAction(nameof(Index));
    }
    #endregion
}
