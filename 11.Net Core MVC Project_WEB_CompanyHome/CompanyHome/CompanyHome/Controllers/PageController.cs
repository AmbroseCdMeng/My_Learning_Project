using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CompanyHome.Core
{
    public class PageController : Controller
    {
        private MyDBContent myDBContent;
        public PageController(MyDBContent myDBContent) {
            this.myDBContent = myDBContent;
        }

        public IActionResult Index(string identity)
        {
            var page = myDBContent.Pages.FirstOrDefault(m => m.IDENTITY == identity);
            return View(page);
        }
    }
}