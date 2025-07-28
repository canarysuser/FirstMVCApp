using System.Diagnostics;
using FirstMVCApp.Infrastructure;
using FirstMVCApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace FirstMVCApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DependencyClass _dc;

        public HomeController(ILogger<HomeController> logger, DependencyClass dc)
        {
            _logger = logger;
            _dc = dc;
        }
        public string GetGuid()
        {
            return _dc.GetId();
        }


        //Action Methods 
        public IActionResult Index()
        {
            _logger.LogInformation("Index Action Method Invoked");
            //Conventionally, Name of View matches with the name of the action
            return View();  
            //Index View will be stored in the Home Folder under the Views Folder 
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //Action Methods 
        //URL:  /home/Today
        public string Today()
        {
            return DateTime.Now.ToString();
        }
        //URL:  /home/greeting/Epsilon
        //ModelBinders - find the value of the parameter from the URL and pass it to the action method
        // If the parameter is not found, it will search for the value in the query string and pass it
        // If the parameter is not found in both places, it will search for the value in the form data
        // If the parameter is not found in all three places, it will pass null or throw exception based on the action method signature
        //URL: /home/greeting/?name=Epsilon

      
        public string Greeting(string name /*id*/)
        {
            return $"Hello {name/*id*/}, Welcome to FirstMVCApp";
        }
        //URL:  /home/AllData
        public IActionResult AllData()
        {
            var obj = new { Id=101, Name="Epsilon", Age=25, Address="Chennai" };
            return Json(obj);
        }
        //URL:  /home/Redirect
        public IActionResult Redirect()
        {
            return RedirectToAction("Index", "Home");
        }

        public IActionResult About()
        {
            return View(viewName: "About1");
        }

        //URL:  /home/Products  
        public IActionResult Products()
        {
            var model = new Product
            {
                ProductId = 1,
                Discontinued = false,
                ProductName = "Chai",
                UnitsInStock = 39,
                UnitPrice = 18.00M
            };
            return View(model: model);
        }

    }
}
