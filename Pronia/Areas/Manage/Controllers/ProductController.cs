using Microsoft.AspNetCore.Mvc;

namespace Pronia.Areas.Manage.Controllers
{
    [Area("manage")]
    public class ProductController : Controller
    {
        AppDbContext _db;
        public ProductController(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            List<Product> products = await _db.Products.Include(p => p.Category)
                 .Include(p => p.ProductTags).ThenInclude(p => p.Tag).ToListAsync();
            return View(products);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _db.Categories.ToListAsync();
            ViewBag.Tags = await _db.Tags.ToListAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductVm vm)
        {
            ViewBag.Categories = await _db.Categories.ToListAsync();
            ViewBag.Tags = await _db.Tags.ToListAsync();
            if (!ModelState.IsValid)
            {
                return View();
            }
            bool result = await _db.Categories.AnyAsync(c => c.Id == vm.CategoryId);
            if (!result)
            {
                ModelState.AddModelError("CategoryId", "Bele category movcuud deyil");
            }
            Product product = new Product()
            {
                Name = vm.Name,
                Price = vm.Price,
                Description = vm.Description,
                SKU = vm.SKU,
                CategoryId = vm.CategoryId,
            };
            await _db.Products.AddAsync(product);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int id)
        {
            Product product = await _db.Products.Where(p => p.Id == id).FirstOrDefaultAsync();
            if (product == null)
            {
                return View("Error");
            }
            ViewBag.Categories = await _db.Categories.ToListAsync();
            ViewBag.Tags = await _db.Tags.ToListAsync();
            UpdateProductVm vm = new UpdateProductVm()
            {
                Id = id,
                Name = product.Name,
                Description = product.Description,
                SKU = product.SKU,
                CategoryId = product.CategoryId
            };
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateProductVm vm)
        {
            ViewBag.Categories = await _db.Categories.ToListAsync();
            ViewBag.Tags = await _db.Tags.ToListAsync();
            if (!ModelState.IsValid)
            {
                return View("Error");
            }
            Product product = await _db.Products.Where(p => p.Id == vm.Id).FirstOrDefaultAsync();
            if (product == null)
            {
                return View("Error");
            }
            bool result = await _db.Categories.AnyAsync(c => c.Id == vm.CategoryId);
            if (!result)
            {
                ModelState.AddModelError("CategoryId", "Bele category movcuud deyil");
            }
            product.Name = vm.Name;
            product.Description = vm.Description;
            product.Price = vm.Price;
            product.SKU = vm.SKU;
            product.CategoryId = vm.CategoryId;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            var product = _db.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return View("Error");
            }
            _db.Products.Remove(product);
            _db.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
