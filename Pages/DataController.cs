using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Corona.Pages
{
    public class DataController : Controller
    {
        public IActionResult Index()
        {
            Console.WriteLine("Hello World");
            return View();
        }
    }
}
