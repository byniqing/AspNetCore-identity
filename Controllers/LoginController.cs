using IdentityDemo.Models;
using IdentityDemo.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityDemo.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signinManager;
        public LoginController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signinManager)
        {
            _userManager = userManager;
            _signinManager = signinManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
           var user =  _userManager.FindByEmailAsync(model.Email).Result;

            if (user!=null)
            {
                var claims = new List<Claim> {
                    new Claim("name",model.UserName)
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var cliamsPrincipal = new ClaimsPrincipal(claimsIdentity);

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, cliamsPrincipal);

                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index");
        }

        public IActionResult Registe()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Registe(LoginViewModel model)
        {
            var identityUser = new ApplicationUser
            {
                UserName = model.UserName,
                PasswordHash = model.PassWorld,
                NormalizedEmail = model.UserName
            };

            var identityResult = await _userManager.CreateAsync(identityUser, model.PassWorld);
            if (identityResult.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}