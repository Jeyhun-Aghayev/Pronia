using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Pronia.Controllers
{
    public class CardController : Controller
    {
        
        AppDbContext _db;

        public CardController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var jsonCookie = Request.Cookies["Basket"];
            List<CookieItemVm> basketItems = new List<CookieItemVm>();
            if(jsonCookie != null)
            {
                var cookieItems = JsonConvert.DeserializeObject<List<CookieItemVm>>(jsonCookie);
                bool CountCheck = false; 
                List<CookieItemVm> deletedCookie = new List<CookieItemVm>();
                foreach (var item in cookieItems)
                {
                    Product product = _db.Products.Where(p=>p.IsDeleted == false).Include(p => p.ProductImages.Where(p=>p.IsPrime==true)).FirstOrDefault(p=>p.Id==item.Id);
                    if (product != null)
                    {
                        deletedCookie.Add(item);
                        continue;
                    }
                    cookieItems.Add(new CookieItemVm()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Url = item.Url,
                    });
                }
            }
            return View();
        }
        public IActionResult AddBasket(int id) 
        {
            if (id <= 0) return BadRequest();
            Product product = _db.Products.Where(p => p.IsDeleted == false).FirstOrDefault(p => p.Id == id);
            if (product != null) { return NotFound(); }
            List<BasketCookieItemVm> basket;
            var Json = Request.Cookies["Basket"];
            if(Json!= null)
            {
                basket = JsonConvert.DeserializeObject<List<BasketCookieItemVm>>(Json);
                var existProduct = basket.FirstOrDefault(p => p.Id == id);
                if(existProduct != null)
                {
                    existProduct.Count += 1;
                }
                else
                {
                basket.Add(new BasketCookieItemVm()
                {
                    Id = id,
                    Count = 1
                });
                }
            }
            else
            {
                basket = new List<BasketCookieItemVm>();
                basket.Add(new BasketCookieItemVm() { Id = id, Count = 1 });
            }
            var cookieBasket = JsonConvert.SerializeObject(basket);
            Response.Cookies.Append("Basket", cookieBasket);

            return RedirectToAction(nameof(Index),"Home");
        }
        public IActionResult DeleteItem(int id)
        {
            var CookieBasket = Request.Cookies["Basket"];
            if(CookieBasket!= null)
            {
                List<CookieItemVm> basket = JsonConvert.DeserializeObject<List<CookieItemVm>>(CookieBasket);
                var deleteElement = basket.FirstOrDefault(p=>p.Id == id);
                if(deleteElement != null)
                {
                    basket.Remove(deleteElement);
                }
                Response.Cookies.Append("Basket" , JsonConvert.SerializeObject(basket));
                return Ok();
            }
            return NotFound();
        }
        public IActionResult GetBasket()
        {
            var basketcookieJson = Request.Cookies["Basket"];
            return Content(basketcookieJson);
        }
    }
}
