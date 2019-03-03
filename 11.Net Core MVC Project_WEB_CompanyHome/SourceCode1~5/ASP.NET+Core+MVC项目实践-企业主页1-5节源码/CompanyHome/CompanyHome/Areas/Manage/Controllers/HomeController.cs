using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CompanyHome.Areas.Manage.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Main()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string userName,string PasswordFor,string Verification)
        {
            if (!Verification.Equals(TempData["code"]))
            {
                return Content("验证码不对");
            }
            return View();
        }
    }
}