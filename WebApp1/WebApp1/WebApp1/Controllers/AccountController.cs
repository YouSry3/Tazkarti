using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApp1.Models;
using WebApp1.Models.DataBase;
using WebApp1.Request;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace WebApp1.Controllers
{
	public class AccountController : Controller
	{
        
		DataBase DB = new DataBase();
		[HttpGet]
		public IActionResult Register() {
		   
			return View("Register");
		}
		[HttpPost]
		public IActionResult Register(Request.RegisterRequest Request)
		{
			var user = new User()
			{
				Name = Request.Name,
				Email = Request.Email,
				Password = Request.Password,
				role = Request.role,

			};
			DB.Users.Add(user);
			DB.SaveChanges();

			return RedirectToAction("Index","Home");
		}

		[HttpGet]
		public IActionResult Login()
		{

			
			return View("Login");
		}
		[HttpPost]
		public async Task<IActionResult> LoginAsync(Request.LoginRequest Request)
		{
			var foundeduser = DB.Users.FirstOrDefault(x => x.Email == Request.Email);
			if(foundeduser == null)
			{
                ModelState.AddModelError("", "This Email Or Password Uncorrect!");

                return View(Request);

			}

    List<Claim> claims = [
    new Claim(ClaimTypes.Email, foundeduser.Email.ToString()),
                new Claim(ClaimTypes.Sid, foundeduser.Id.ToString()),
                new Claim(ClaimTypes.Role, foundeduser.role.ToString())
    ];
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var Principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, Principal);
			string Email = foundeduser.Email.Replace(".com", "");
			
			HttpContext.Response.Cookies.Append("Email", Email);

            




            return RedirectToAction("Index", "Home");

        }


        public async Task<IActionResult> Logout()
        {
            // تسجيل الخروج
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // إعادة التوجيه لصفحة تسجيل الدخول أو الصفحة الرئيسية بعد تسجيل الخروج
            return RedirectToAction("Index", "Home");
        }
    }
}
