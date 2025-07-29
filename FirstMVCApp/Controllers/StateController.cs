using Microsoft.AspNetCore.Mvc;

namespace FirstMVCApp.Controllers
{
    public class StateController : Controller
    {
        public IActionResult Index(int id=0)
        {
            ViewData["Message"] = "Welcome to the State Controller ViewData!";
            //Dynamic object with dynamic properties - Expando Object 
            ViewBag.Message = "This is a message from the State Controller ViewBag!";

            TempData["Message"] = "This is a temporary message from the State Controller!";
            HttpContext.Session.SetString("Message", "This is a session message from the State Controller!");
            if (id == 1)
                return RedirectToAction("Contact");
            return View() ; //==> ViewBag.Model = model
        }

        public IActionResult Contact(int id)
        {
            
            return View();
        }
    }
}
