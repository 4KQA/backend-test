using Microsoft.AspNetCore.Mvc;

namespace Nerd.Api.Controllers
{
    public class ErrorsController : ControllerBase
    {
        [HttpGet("/error")]
        public IActionResult Error() => Problem();
    }
}