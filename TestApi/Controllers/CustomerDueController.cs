using Microsoft.AspNetCore.Mvc;

namespace BaseApi.Controllers
{
    public class CustomerDueController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
