using Microsoft.AspNetCore.Mvc;
using OGWeb.Core.Wrappers;

namespace OGWeb.Api.Controllers;

[ApiController, Route("api/[controller]")]
public class CustomControllerBase : ControllerBase
{
    public IActionResult CreateActionResult<T>(CustomResponse<T> response)
    {
        return new ObjectResult(response.StatusCode == 204 ? null : response)
        {
            StatusCode = response.StatusCode
        };
    }
}