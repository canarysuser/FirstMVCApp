using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        
        public async Task<IActionResult> Login(Models.LoginViewModel model)
        {



            if (ModelState.IsValid)
            {
                if(model.Username == "admin" && model.Password == "admin")
                {
                    
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, model.Username),
                        new Claim(ClaimTypes.Role, "Admin")
                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var principal = new ClaimsPrincipal(identity);


                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        principal,
                        new AuthenticationProperties
                        {
                            IsPersistent = model.RememberMe, // Remember me option
                            ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60) 
                        }
                    );

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
