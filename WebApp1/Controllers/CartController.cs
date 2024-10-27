using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApp1.Models.DataBase;

namespace WebApp1.Controllers
{
    public class CartController : Controller
    {
        DataBase DB = new DataBase();
        public IActionResult Cart()
        {
            var sidClaim = int.Parse(User.FindFirst(ClaimTypes.Sid)?.Value);

            // جلب المنتجات الموجودة في السلة لهذا المستخدم
            var cartItems = DB.DetailsUser.Where(x => x.UserId == sidClaim).ToList();

            // حساب المجموع الإجمالي للسلة
            decimal totalAmount = cartItems.Sum(x => x.Price); // افترض أن كل منتج يحتوي على حقل Price

            ViewBag.TotalAmount = totalAmount;
            return View(cartItems);
        }

        [HttpPost]
        public IActionResult Checkout()
        {
            var sidClaim = int.Parse(User.FindFirst(ClaimTypes.Sid)?.Value);

            // جلب المنتجات الموجودة في السلة
            var cartItems = DB.DetailsUser.Where(x => x.UserId == sidClaim).ToList();

            if (cartItems == null || !cartItems.Any())
            {
                return RedirectToAction("Cart");
            }

            // حساب المجموع الإجمالي للسلة
            decimal totalAmount = cartItems.Sum(x => x.Price);

            // إرسال المبلغ الإجمالي إلى صفحة الدفع
            return RedirectToAction("Payment", new { totalAmount = totalAmount });
        }


        public IActionResult Payment(decimal totalAmount)
        {
            ViewBag.TotalAmount = totalAmount;
            return View();
        }


    }
}
