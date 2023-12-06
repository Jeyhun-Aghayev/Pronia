using Microsoft.AspNetCore.Mvc;

namespace Pronia.Controllers
{
    public class ShopController : Controller
    {
        AppDbContext _db;
        public ShopController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Detail(int? id)
        {
            Product product  = _db.Products
                .Where(p => p.IsDeleted == false)
                .Include(p => p.ProductImages)
                .Include(p => p.Category)
                .Include(p => p.ProductTags)
                .ThenInclude(p => p.Tag)
                .FirstOrDefault(p=>p.Id==id);
                
            
            return View(product);
        }
    }
}
