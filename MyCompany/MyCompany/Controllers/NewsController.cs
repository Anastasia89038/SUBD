using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCompany.Domain;
using MyCompany.Domain.Entities;
using MyCompany.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyCompany.Controllers
{
    public class NewsController : Controller
    {
        private readonly DataManager _dataManager;
        private readonly IWebHostEnvironment _hostingEnv;

        public NewsController(DataManager dataManager, IWebHostEnvironment hostingEnvironment)
        {
            _dataManager = dataManager;
            _hostingEnv = hostingEnvironment;
        }
        public IActionResult Index(string id)
        {
            if (id != default)
            {
                return View("Show", _dataManager.NewsItems.Get(id));
            }

            ViewBag.TextField = _dataManager.TextFields.Get("PageNews");
            return View(_dataManager.NewsItems.GetAll());
        }

        public IActionResult AllNews()
        {
            return View(_dataManager.NewsItems.GetAll());
        }
        public IActionResult Edit(string id)
        {
            var entity = id == default ? new NewsItem() : _dataManager.NewsItems.Get(id);
            return View(entity);
        }

        [HttpPost]
        public IActionResult Edit(NewsItem model, IFormFile titleImageFile)
        {
            if (ModelState.IsValid)
            {
                if (titleImageFile != null)
                {
                    var newName = Guid.NewGuid().ToString() + Path.GetExtension(titleImageFile.FileName);
                    model.TitleImagePath = newName;
                    using (var stream = new FileStream(Path.Combine(_hostingEnv.WebRootPath, "images/news", newName), FileMode.Create))
                    {
                        titleImageFile.CopyTo(stream);
                    }
                }
                if (model.Id == null)
                    _dataManager.NewsItems.Create(model);
                else
                    _dataManager.NewsItems.Update(model.Id, model);
                return RedirectToAction(nameof(NewsController.AllNews), nameof(NewsController).CutController());
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(string id)
        {
            _dataManager.ServiceItems.Delete(id);
            return RedirectToAction(nameof(NewsController.AllNews), nameof(NewsController).CutController());
        }
    }
}
