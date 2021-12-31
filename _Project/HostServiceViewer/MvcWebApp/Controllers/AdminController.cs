using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcWebApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MvcWebApp.Controllers
{
    public class AdminController : Controller
    {
        
        
        public IActionResult Index()
        {
            return View();
        }


        
    }
}
