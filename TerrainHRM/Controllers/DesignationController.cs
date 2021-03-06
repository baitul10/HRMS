using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TerrainHRM.Interfaces;
using TerrainHRM.Models;

namespace TerrainHRM.Controllers
{
    public class DesignationController : Controller
    {
        private readonly IDesigRepository _desig;
        public DesignationController(IDesigRepository desig)
        {
            _desig = desig;
        }
        public IActionResult Index()
        {
            var desigList = _desig.GetDesigList();
            return View(desigList);
        }

        public IActionResult CreateUpdateDesignation(List<DesigMst> desigs)
        {
            _desig.CreateUpdateDesig(desigs);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult DeleteDesignation(int id)
        {
            var result = _desig.DeleteDesig(id);
            if (result<=0)
            {
                throw new Exception("Unauthorized error occurred!");
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
