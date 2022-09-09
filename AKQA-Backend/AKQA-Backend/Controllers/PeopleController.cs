using Microsoft.AspNetCore.Mvc;

namespace AKQA_Backend.Controllers
{
    public class PeopleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
