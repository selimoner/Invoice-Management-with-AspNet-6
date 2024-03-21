using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using testProject.ViewModels.AuthenticationViewModels;
using testProject.Repositories.UserRepository;
using Microsoft.EntityFrameworkCore;

namespace testProject.Controllers
{
    public class AccessController : Controller
    {
        private readonly IUserCollection database;

        public AccessController(IUserCollection userCollection) 
        {
            database = userCollection; 
        }
        public IActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            if (claimUser.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
                //return RedirectToAction("Index", "Post");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel modelLogin)
        {
            var user = database.GetUserByUsername(modelLogin.User.Username);
            if (user == null)
            {
                ViewData["ValidateMessage"] = "User not found in Imex database.";
                return View();
            }
            string userUsername = user.Username.ToString().ToLower();
            string userPassword = user.Password.ToString().ToLower();

            string inputUsername = modelLogin.User.Username.ToString().ToLower();
            string inputPassword = modelLogin.User.Password.ToString().ToLower();

            if (inputUsername == userUsername && inputPassword == userPassword)
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Username),
                    new Claim("OtherProperties","Example Role")
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme
                );

                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = modelLogin.KeepLoggedIn,
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);

                return RedirectToAction("Index", "Home");
            }

            ViewData["ValidateMessage"] = "User not registered in Imex database.";
            return View();
        }
    }
}
