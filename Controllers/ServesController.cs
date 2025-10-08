using Microsoft.AspNetCore.Mvc;

namespace Cinema_Scope.Controllers
{
    public class ServecController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
