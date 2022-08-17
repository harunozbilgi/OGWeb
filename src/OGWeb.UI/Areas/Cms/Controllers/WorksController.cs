using MediatR;
using Microsoft.AspNetCore.Mvc;
using OGWeb.Core.Commands.Works;
using OGWeb.Core.Queries.Works;

namespace OGWeb.UI.Areas.Cms.Controllers;

[Area("Cms")]
public class WorksController : Controller
{
    private readonly IMediator _mediator;

    public WorksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IActionResult> Index()
    {
        var response = await _mediator.Send(new GetAllWorkQuery());
        return View(response.Data.ToList());
    }

    #region Create
    public IActionResult Create()
    {
        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateWorkCommand createWork)
    {
        var work = await _mediator.Send(createWork);

        return RedirectToAction(nameof(Index));
    }

    #endregion

    #region Edit
    public async Task<IActionResult> Edit(Guid id)
    {
        var response = await _mediator.Send(new GetWorkByIdQuery() { Id = id });
        if (response.Data == null)
            return NotFound();

        var work = response.Data;

        var UpdateWorkCommand = new UpdateWorkCommand()
        {
            Id = response.Data.Id,
            Title = work.Title,
            Keyword_Seo = work.Keyword_Seo,
            CreatedDate = work.CreatedDate,
            Description = work.Description,
            Description_Seo = work.Description_Seo,
            IsActived = work.IsActived,
        };

        ViewData["ListImage"] = work.WorkFiles.ToList();

        return View(UpdateWorkCommand);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(UpdateWorkCommand updateWork)
    {
        var work = await _mediator.Send(updateWork);

        return RedirectToAction(nameof(Index));
    }


    #endregion

    #region Delete

    public async Task<IActionResult> Delete(Guid id)
    {
        var response = await _mediator.Send(new DeleteWorkCommand() { Id = id });
        return RedirectToAction(nameof(Index));
    }
    #endregion
}
