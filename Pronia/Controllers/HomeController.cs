namespace Pronia.Controllers
{
    public class HomeController : Controller
    {
        AppDbContext _db;
        public HomeController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            HomeVM vm = new HomeVM()
            {
                Sliders = _db.Sliders.ToList(),
                Products = _db.Products
                .Include(p => p.ProductImages)
                .Include(p => p.Category)
                .Include(p => p.ProductTags)
                .ThenInclude(p => p.Tag)
                .ToList()
            };
            return View(vm);
        }
    }
}
