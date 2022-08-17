using MediatR;
using Microsoft.AspNetCore.Mvc;
using OGWeb.Core.Commands.AppSettings;
using OGWeb.Core.Queries.AppSettings;

namespace OGWeb.UI.Areas.Cms.Controllers;

[Area("Cms")]
public class AppSettingsController : Controller
{
    private readonly IMediator _mediator;

    public AppSettingsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IActionResult> Index()
    {
        var response = await _mediator.Send(new GetAppSettingByQuery());

        var appSetting = response.Data;

        AppSettingCommand settingCommand = new()
        {
            LogoUrl = appSetting.LogoUrl,
            FaceBook = appSetting.FaceBook,
            GooglePixel = appSetting.GooglePixel,
            Instagram = appSetting.Instagram,
            LinkedIn = appSetting.LinkedIn,
            Twitter = appSetting.Twitter,
            YouTube = appSetting.YouTube
        };

        return View(settingCommand);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(AppSettingCommand request)
    {
        await _mediator.Send(request);
        return RedirectToAction(nameof(Index));
    }
}
