using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
<<<<<<< HEAD
using System.Globalization;

namespace Pronia.Areas.Manage.Controllers
{
	[Area("Manage")]
	public class SettingController : Controller
	{
		AppDbContext _db;

		public SettingController(AppDbContext db)
		{
			_db = db;
		}

		public IActionResult Index()
		{
			List<Setting> list = _db.Setting.ToList();

			return View(list);
		}

		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Create(CreateSettingVm createSetting)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}
			bool keyExsist = _db.Setting.Any(p => p.Key.Trim().ToLower() == createSetting.Key.Trim().ToLower());
			if (keyExsist)
			{
				ModelState.AddModelError("Key", "bu key movcuddur,keyler benzersiz olmalidir");
			}
			Setting setting = new Setting()
			{
				Key = MakeFirstLetterUppercase(createSetting.Key),
				Value = createSetting.Value,
			};
			_db.Setting.Add(setting);
			_db.SaveChanges();
			return RedirectToAction("Index");
		}
		public IActionResult Update(int id)
		{
			if (!_db.Setting.Any(p=>p.Id==id))
			{
				return RedirectToAction("Index");
			}
			Setting setting = _db.Setting.Where(p=>p.Id==id).FirstOrDefault();
			return View(setting);
		}
		[HttpPost]
		public IActionResult Update(Setting newSetting)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}
			Setting oldSetting = _db.Setting.Find(newSetting.Key);
			if (oldSetting == null)
			{
				return View();
			}
			oldSetting.Value = newSetting.Value;
			_db.SaveChanges();
			return RedirectToAction("Index");
		}
		public IActionResult Delete(int Id)
		{
			var setting = _db.Setting.FirstOrDefault(s => s.Id == Id);
			if (setting == null)
			{
				return View();
			}
			_db.Setting.Remove(setting);
			_db.SaveChanges();
			return RedirectToAction("Index");
		}
		public static string MakeFirstLetterUppercase(string input)
		{
			if (string.IsNullOrEmpty(input))
			{
				return input;
			}

			TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
			return textInfo.ToTitleCase(input.ToLower());
		}

	}
=======

namespace Pronia.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SettingController : Controller
    {
        AppDbContext _db;

        public SettingController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<Setting> list = _db.Setting.ToList(); 

            return View();
        }

        public IActionResult Cerate() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Setting setting)
        {
            if (!ModelState.IsValid)
            {
                _db.Setting.Add(setting);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(setting);
        }
        public IActionResult Update(string Key)
        {
            Setting setting = _db.Setting.Find(Key);
            if (setting == null)
            {
                return View();
            }
            return View(setting);
        }


        [HttpPost]
        public IActionResult Update(Setting newSetting)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Setting oldSetting = _db.Setting.Find(newSetting.Key);
            if (oldSetting == null)
            {
                return View();
            }
            oldSetting.Key = newSetting.Key;
            oldSetting.Value = newSetting.Value;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(string Key)
        {
            var setting = _db.Setting.FirstOrDefault(s => s.Key == Key);
            if (setting == null)
            {
                return View();
            }
            _db.Setting.Remove(setting);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
>>>>>>> 96a64841f28dbec61ff07e9403351c3c7bed7599

}
