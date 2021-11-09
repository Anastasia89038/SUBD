using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCompany.Domain;
using MyCompany.Domain.Entities;
using MyCompany.Service;

namespace MyCompany.Controllers
{
    public class ServicesController : Controller
    {
        private readonly DataManager _dataManager;
        private readonly IWebHostEnvironment _hostingEnv;

        public ServicesController(DataManager dataManager, IWebHostEnvironment hostingEnvironment)
        {
            _dataManager = dataManager;
            _hostingEnv = hostingEnvironment;
        }

        public IActionResult Index(string id)
        {
            if (id != default)
            {
                return View("Show", _dataManager.ServiceItems.Get(id));
            }

            ViewBag.TextField = _dataManager.TextFields.Get("PageServices");
            return View(_dataManager.ServiceItems.GetAll());
        }

        public IActionResult AllServices()
        {
            return View(_dataManager.ServiceItems.GetAll());
        }
        public IActionResult Edit(string id)
        {
            var entity = id == default ? new ServiceItem() : _dataManager.ServiceItems.Get(id);
            return View(entity);
        }

        [HttpPost]
        public IActionResult Edit(ServiceItem model, IFormFile titleImageFile)
        {
            if (ModelState.IsValid)
            {
                if (titleImageFile != null)
                {
                    var newName = Guid.NewGuid().ToString() + Path.GetExtension(titleImageFile.FileName);
                    model.TitleImagePath = newName;
                    using (var stream = new FileStream(Path.Combine(_hostingEnv.WebRootPath, "images/service", newName), FileMode.Create))
                    {
                        titleImageFile.CopyTo(stream);
                    }
                }
                if (model.Id == null)
                    _dataManager.ServiceItems.Create(model);
                else
                    _dataManager.ServiceItems.Update(model.Id, model);
                return RedirectToAction(nameof(ServicesController.AllServices), nameof(ServicesController).CutController());
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(string id)
        {
            _dataManager.ServiceItems.Delete(id);
            return RedirectToAction(nameof(ServicesController.AllServices), nameof(ServicesController).CutController());
        }
    }
}