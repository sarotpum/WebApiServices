using Microsoft.AspNetCore.Mvc;
using SharedService.Models.Student;
using System.Diagnostics;

namespace WebApiServices.Controllers.BasicASPTutorialController
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;   
        }

        public IActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP";
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public ActionResult Anyting()
        {
            return Content("Hello World!!!");
        }

        public ActionResult Something()
        {
            return Content("Something...");
        }

        public ActionResult GetBookDetail()
        {
            BooksModel obj = new BooksModel()
            {
                BookNumber = 1,
                BookName = "Book MVC",
                BookAuther = "Sarot",
                Price = Convert.ToDecimal(200.20)
            };

            return View(obj);
        }
         
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
