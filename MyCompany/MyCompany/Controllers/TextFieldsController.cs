using Microsoft.AspNetCore.Mvc;
using MyCompany.Domain;
using MyCompany.Domain.Entities;
using MyCompany.Service;
using System.Collections.Generic;

namespace MyCompany.Controllers
{
    public class TextFieldsController : Controller
    {
        private readonly DataManager _dataManager;

        public TextFieldsController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public IActionResult Fields() => View(_dataManager.TextFields.GetAll());

        public IActionResult Edit(string codeWord)
        {
            var entity = _dataManager.TextFields.Get(codeWord);
            return View(entity);
        }

        [HttpPost]
        public IActionResult Edit(TextField model)
        {
            if (ModelState.IsValid)
            {
                _dataManager.TextFields.Update(model.Id, model);
                return RedirectToAction(nameof(TextFieldsController.Fields), nameof(TextFieldsController).CutController());
            }
            return View(model);
        }
    }
}
