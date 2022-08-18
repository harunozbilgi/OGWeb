using MediatR;
using Microsoft.AspNetCore.Mvc;
using OGWeb.Core.Queries.OverViews;
using OGWeb.Core.Queries.Sliders;
using OGWeb.Core.Queries.Videos;
using OGWeb.Core.Queries.Works;
using OGWeb.UI.Models;
using System.Diagnostics;

namespace OGWeb.UI.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IMediator _mediator;

    public HomeController(ILogger<HomeController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [Route("/")]
    public async Task<IActionResult> Index()
    {
        _logger.LogInformation("Ana Sayfa");

        var response = await _mediator.Send(new GetAllSliderQuery());

        return View(response.Data.ToList());
    }

    [Route("/works")]
    public async Task<IActionResult> Work()
    {
        _logger.LogInformation("Ana Sayfa");

        var response = await _mediator.Send(new GetAllWorkQuery());

        return View(response.Data.ToList());
    }

    [Route("/works/{id}")]
    public async Task<IActionResult> WorkDetail(Guid id)
    {
        _logger.LogInformation("Ana Sayfa");

        var response = await _mediator.Send(new GetWorkByIdQuery() { Id = id });

        return View(response.Data);
    }

    [Route("/overview")]
    public async Task<IActionResult> OverView()
    {
        _logger.LogInformation("Hakkimizda");

        var response = await _mediator.Send(new GetAllOverViewQuery());

        return View(response.Data.ToList());
    }

    [Route("/videos")]
    public async Task<IActionResult> Video()
    {
        _logger.LogInformation("Video");

        var response = await _mediator.Send(new GetAllVideoQuery());

        return View(response.Data.ToList());
    }

    [Route("/contact")]
    public IActionResult Contact()
    {
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}