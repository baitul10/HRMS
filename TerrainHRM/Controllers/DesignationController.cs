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
        private readonly IDivisionRepository _division;

        public DesignationController(IDesigRepository desig, IDivisionRepository division)
        {
            _desig = desig;
            _division = division;
        }
        public IActionResult Index()
        {
            var desigList = _desig.GetDesigList();
            return View(desigList);
        }

        [HttpPost]
        public IActionResult CreateUpdateDesignation(List<DesigMst> desigs)
        {
            var desigList = _desig.CreateUpdateDesig(desigs);
            return Ok(desigList);
        }

        [HttpPost]
        public IActionResult DeleteDesignation(int id)
        {
            var result = _desig.DeleteDesig(id);
            if (result<=0)
            {
                throw new Exception("Unauthorized error occurred!");
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult DivisionIndex()
        {
            var divisions = _division.GetDivisions();
            return View("DivisionView", divisions);
        }

    }

}
