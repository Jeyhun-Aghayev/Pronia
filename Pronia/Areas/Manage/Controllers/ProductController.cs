using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace Pronia.Areas.Manage.Controllers
{
    [Area("manage")]
    public class ProductController : Controller
    {
        AppDbContext _db;
        IWebHostEnvironment _environment;
                public ProductController(AppDbContext db,IWebHostEnvironment environment)
        {
            _db = db;
            _environment = environment;
        }
        public async Task<IActionResult> Index()
        {
            List<Product> products = await _db.Products.Where(p=>p.IsDeleted==false).Include(p => p.Category)
                .Include(p => p.ProductImages)
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
            if (vm.MainImage.CheckType("image/"))
            {
                ModelState.AddModelError("MainImage", "Yanliz sekil daxil ede bilersiz");
                return View(vm);
            }
            if (vm.MainImage.CheckLength(3000))
            {
                ModelState.AddModelError("MainImage", "Max 3mb sekil ywkleye bilersiniz");
            }
            if (vm.MainImage.CheckType("image/"))
            {
                ModelState.AddModelError("SecondImage", "Yanliz sekil daxil ede bilersiz");
                return View(vm);
            }
            if (vm.MainImage.CheckLength(3000))
            {
                ModelState.AddModelError("SecondImage", "Max 3mb sekil ywkleye bilersiniz");
            }
            Product product = new Product()
            {
                Name = vm.Name,
                Price = vm.Price,
                Description = vm.Description,
                SKU = vm.SKU,
                CategoryId = vm.CategoryId,
                ProductImages = new List<ProductImage>()
            };
            foreach (var item in vm.DetailImages)
            {
                if (item.CheckType("image/"))
                {
                    TempData["Error"] += $"{item.Name} type duzgun deyil";
                    continue;
                }
                if (item.CheckLength(3000))
                {
                    TempData["Error"] += $"{item.Name} 3mb-dan yxaridir";
                    continue;
                }
                ProductImage productImage = new ProductImage()
                {
                    IsPrime = null,
                    Url = item.Upload(_environment.WebRootPath,@"\Upload\ProductImage\"),
                    Product = product
                };
                product.ProductImages.Add(productImage);
            }
                ProductImage pi = new ProductImage()
            {
                IsPrime = true,
                Url = vm.MainImage.Upload(_environment.WebRootPath,@"\Upload\ProductImage\"),
                Product = product
            };
            ProductImage pisecond = new ProductImage()
            {
                IsPrime = false,
                Url = vm.MainImage.Upload(_environment.WebRootPath, @"\Upload\ProductImage\"),
                Product = product
            };
            product.ProductImages.Add(pi);
            product.ProductImages.Add(pisecond);          
            await _db.Products.AddAsync(product);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int id)
        {
            Product product = await _db.Products.Where(p => p.IsDeleted == false).Where(p => p.Id == id).Include(p=>p.ProductTags).ThenInclude(p=>p.Tag).Include(p=>p.ProductImages).FirstOrDefaultAsync();
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
            Product product = await _db.Products.Where(p => p.IsDeleted == false).Where(p => p.Id == vm.Id).Include(p => p.ProductTags).ThenInclude(p => p.Tag).Include(p => p.ProductImages).FirstOrDefaultAsync();
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
        public IActionResult DeleteSuperVisor(int id)
        {
            var product = _db.Products.Where(p => p.IsDeleted == false).FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return View("Error");
            }
            _db.Products.Remove(product);
            _db.SaveChanges();
            return RedirectToAction("index");

        }
        public IActionResult Delete(int id)
        {
            var product = _db.Products.Where(p => p.IsDeleted == false).FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return View("Error");
            }
            product.IsDeleted = true;
            _db.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
