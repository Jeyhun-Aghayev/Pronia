using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Pronia.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class CategoryController : Controller
    {
        AppDbContext _db;

        public CategoryController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<Category> categories = _db.Categories.Include(p=>p.Products).ToList();
            return View(categories);
        }
        public IActionResult Create() 
        { 
            
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            _db.Categories.Add(category);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id) 
        {
            
            Category category = _db.Categories.Find(id);
            _db.Categories.Remove(category);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Update(int id) 
        {
            Category category = _db.Categories.Find(id);
            return View(category);
        }
        [HttpPost]
        public IActionResult Update(Category newcategory)
        { 
            if (!ModelState.IsValid)
            {
                return View("Error");
            }
            Category oldcategory = _db.Categories.Find(newcategory.Id);
            oldcategory.Name=newcategory.Name;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
