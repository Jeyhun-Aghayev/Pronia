using Pronia.ViewModels;

namespace Pronia.Controllers
{
    public class HomeController : Controller
    {
        AppDbContext _db;
        public HomeController(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            HomeVM vm = new HomeVM()
            {
                Sliders = await _db.Sliders.ToListAsync(),
                Products = await _db.Products.Where(p => p.IsDeleted == false).Include(p => p.ProductImages).ToListAsync()
            };
            return View(vm);
        }
    }
}
