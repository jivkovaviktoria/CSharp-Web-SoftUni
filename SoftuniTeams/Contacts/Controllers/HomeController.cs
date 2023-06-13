using Contacts.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Contacts.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (this.UserIsAutheticated()) return RedirectToAction("All", "Contact");
            return View();
        }
        
        private bool UserIsAutheticated() => User?.Identity?.IsAuthenticated ?? false;
    }
}