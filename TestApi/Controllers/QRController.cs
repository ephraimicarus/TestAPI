using BaseApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BaseApi.Controllers
{
    [Route("api/[controller]")]
    public class QRController : Controller
    {
        [HttpGet]
        [Route("tripadvisorred")]
        public IActionResult TripAdvisorRed()
        {
            return Redirect("/RedirectPageTripAdvisor.html");
        }

        [HttpGet]
        [Route("googlereviewred")]
        public IActionResult GoogleRevRed()
        {
            return Redirect("/RedirectPageGoogle.html");
        }
    }
}
