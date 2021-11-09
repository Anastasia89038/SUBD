using Microsoft.AspNetCore.Mvc;
using MyCompany.Domain;
using MyCompany.Domain.Entities;
using MyCompany.Domain.Repositories.MongoDb;

namespace MyCompany.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataManager _dataManager;

        public HomeController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public IActionResult Index()
        {
            return View(_dataManager.TextFields.Get("PageIndex"));
        }

        public IActionResult Contacts()
        {
            return View(_dataManager.TextFields.Get("PageContacts"));
        }
    }
}