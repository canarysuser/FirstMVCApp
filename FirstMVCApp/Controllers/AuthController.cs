using Microsoft.AspNetCore.Mvc;

namespace FirstMVCApp.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Login()
        {
            var model = new Models.LoginViewModel();    
            return View();
        }

        [HttpPost]
        public IActionResult Login(Models.LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if(model.Username == "admin" && model.Password == "admin")
                {
                    // Here you would typically set up authentication cookies or tokens
                    // For simplicity, we will just redirect to the home page
                    TempData["Message"] = "Login successful!";
                    HttpContext.Session.SetString("Username", model.Username);
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
                return RedirectToAction(actionName: "Index", controllerName: "Home");
            }
            // If we got this far, something failed; redisplay the form
            return View(model);
        }
    }
}
