using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using WebApp1.Models;
using WebApp1.Models.DataBase;

namespace WebApp1.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}
				
				DataBase DB = new DataBase();

		public IActionResult Index()
		{
			
			if(User.Identity?.IsAuthenticated == true)
			{
                var sidClaim = int.Parse(User.FindFirst(ClaimTypes.Sid)?.Value);



                string ticketCount = DB.DetailsUser.Where(x => x.UserId == sidClaim).Count().ToString();

                string Email = HttpContext.Request.Cookies["Email"];

                if (string.IsNullOrEmpty(ticketCount))
                {
                    ticketCount = "0"; // ?? ???? ??? ???? ??????? ?? ??? Cookies
                }


                ViewBag.Count = ticketCount;
                @ViewBag.UserEmail = Email;
                return View();

            }

            return View();
			
		
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
