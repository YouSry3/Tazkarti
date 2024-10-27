using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApp1.Models;
using WebApp1.Models.DataBase;
using WebApp1.Request;

namespace WebApp1.Controllers
{
    public class TacketsController : Controller
    {
        DataBase DB = new DataBase();
        public IActionResult Index()
        {
            var tasks = DB.Tackets.ToList();
            string ticketCount = HttpContext.Request.Cookies["TicketCount"];
            string Email = HttpContext.Request.Cookies["Email"];
            Email.Replace("@gmail.com", "");
            if (string.IsNullOrEmpty(ticketCount))
            {
                ticketCount = "0"; // في حالة عدم وجود التذاكر في الـ Cookies
            }


            ViewBag.Count = ticketCount;
            @ViewBag.UserEmail = Email;

            return View("Index",tasks);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }
        [HttpPost]
        public IActionResult Create(TacketRequest Request)
        {
            Tacket tacket = new Tacket()
            {
                NameTacket = Request.NameTacket,
                Description = Request.Description,
                Price = Request.Price,
                Palce = Request.Palce,
                Url = Request.Url,
                TimeMatch = DateTime.Now.AddDays(3)
            };
            DB.Tackets.Add(tacket);
            DB.SaveChanges();   

            return RedirectToAction("Index","Tackets");
        }
    


        [HttpPost]
        public IActionResult AddToCart(int id)
        {
            if (User.Identity?.IsAuthenticated != true)
            {
                return RedirectToAction("Login", "Account");
            }

            var tacketFounded = DB.Tackets.FirstOrDefault(x => x.Id == id);
            if(tacketFounded == null)
            {
                ModelState.AddModelError("", "Not Founded Match in Data Base");
                return RedirectToAction("Index",id);
            }
            var sidClaim = int.Parse(User.FindFirst(ClaimTypes.Sid)?.Value);
           
            DetailsTaskUser tacketUser = new DetailsTaskUser()
            {
                NameTacket = tacketFounded.NameTacket,
                TimeMatch = tacketFounded.TimeMatch,
                Description = tacketFounded.Description,
				Price = tacketFounded.Price,
                Url = tacketFounded.Url,
                Palce = tacketFounded.Palce,
                UserId = sidClaim,
            };

            // عد التذاكر المخزنة لهذا المستخدم
            DB.DetailsUser.Add(tacketUser);
            DB.SaveChanges();

            // تحديث عدد التذاكر
            var ticketCount = DB.DetailsUser.Count(x => x.UserId == sidClaim);

            

            // تخزين العدد في الـ Cookies
            HttpContext.Response.Cookies.Append("TicketCount", ticketCount.ToString(), new CookieOptions
            {
                Expires = DateTimeOffset.Now.AddDays(7) // يمكنك تحديد مدة انتهاء الصلاحية كما تريد
            });

            return RedirectToAction("Index");
        }



    }
}
