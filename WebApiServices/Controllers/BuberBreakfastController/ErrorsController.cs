using Microsoft.AspNetCore.Mvc;

namespace WebApiServices.Controllers.BuberBreakfastController
{
    public class ErrorsController : Controller
    {
        //[Route("/error")]
        public IActionResult Error()
        {
            return Problem();
        }
    }
}
