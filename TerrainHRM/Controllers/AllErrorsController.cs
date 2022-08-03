using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TerrainHRM.Models;

namespace TerrainHRM.Controllers
{
    public class AllErrorsController : Controller
    {
        public IActionResult Index(Errors error)
        {
            return View("ErrorView", error);
        }
    }
}
