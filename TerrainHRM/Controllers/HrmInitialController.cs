using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TerrainHRM.DTOs;
using TerrainHRM.Helper;
using TerrainHRM.Interfaces;
using TerrainHRM.Models;

namespace TerrainHRM.Controllers
{
    public class HrmInitialController : Controller
    {
        private readonly IDesigRepository _desig;
        private readonly IDivisionRepository _division;
        private readonly IDeptRepository _dept;
        private readonly ISectRepository _sect;

        public HrmInitialController(IDesigRepository desig, IDivisionRepository division, IDeptRepository dept, ISectRepository sect)
        {
            _desig = desig;
            _division = division;
            _dept = dept;
            _sect = sect;
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
            var divDeptSect = new DivisionDto();
            try
            {
                var divisions = _division.GetDivisions().OrderBy(d => d.DivmId);

                foreach (var div in divisions)
                {
                    divDeptSect.Division.Add(div);
                }

            }
            catch (Exception e)
            {
                var error = new Errors();
                error.ErrorMessage = e.Message;
                error.ErrorEvent = "DivisionIndex Gettting Divisions";
                error.ErrorStackTrace = e.StackTrace;
                return RedirectToAction("Index", "AllErrors", error);
            }

            try
            {
                var departments = _dept.GetListEntity();
                divDeptSect.Departments.AddRange(departments);
            }
            catch (Exception e)
            {
                var error = new Errors();
                error.ErrorMessage = e.Message;
                error.ErrorEvent = "DivisionIndex Gettting Departments";
                error.ErrorStackTrace = e.StackTrace;
                return RedirectToAction("Index", "AllErrors", error);
            }

            try
            {
                var sections = _sect.GetListEntity();
                divDeptSect.Sections.AddRange(sections);
            }
            catch (Exception e)
            {
                var error = new Errors();
                error.ErrorMessage = e.Message;
                error.ErrorEvent = "DivisionIndex Gettting Sections";
                error.ErrorStackTrace = e.StackTrace;
                return RedirectToAction("Index", "AllErrors", error);
            }

            return View("DivisionView", divDeptSect);
        }

        public IActionResult CreateDivDeptSect(List<DivisionMst> divisions, List<DeptMst> departments, List<SectionMst> sections)
        {
            var divDeptSect = new DivisionDto();
            try
            {
                var divisionList = _division.CreatUpdateDivision(divisions);
                divDeptSect.Division = divisionList;
                var deptListToInsert = new List<DeptMst>();
                var deptListToUpdate = new List<DeptMst>();
                var deptId = DbConnectionHelper.GetGeneratedPK("DEPT_MST", "DEPT_ID") + 1;
                foreach (var department in departments)
                {
                    if (department.Id>0)
                    {
                        deptListToUpdate.Add(department);
                    }
                    else
                    {
                        department.Id = deptId;
                        deptListToInsert.Add(department);
                        ++deptId;
                    }
                }
                if (deptListToInsert.Count>0)
                {
                    var insertedDepts = _dept.CreatNewEntity(deptListToInsert);
                    divDeptSect.Departments.AddRange(insertedDepts);
                }
                if (deptListToUpdate.Count>0)
                {
                    var updatedDepts = _dept.UpdateEntity(deptListToUpdate);
                    divDeptSect.Departments.AddRange(updatedDepts);
                }

                var sectListToInsert = new List<SectionMst>();
                var sectListToUpdate = new List<SectionMst>();
                var sectId = DbConnectionHelper.GetGeneratedPK("SECTION_MST", "SECT_ID") + 1;
                foreach (var section in sections)
                {
                    if (section.Id > 0)
                    {
                        sectListToUpdate.Add(section);
                    }
                    else
                    {
                        section.Id = sectId;
                        sectListToInsert.Add(section);
                        ++sectId;
                    }
                }
                if (sectListToInsert.Count > 0)
                {
                    var insertedSects = _sect.CreatNewEntity(sectListToInsert);
                    divDeptSect.Sections.AddRange(insertedSects);
                }
                if (sectListToUpdate.Count > 0)
                {
                    var updatedSects = _sect.UpdateEntity(sectListToUpdate);
                    divDeptSect.Sections.AddRange(updatedSects);
                }
            }
            catch(Exception e)
            {
                var error = new Errors();
                error.ErrorMessage = e.Message;
                error.ErrorEvent = "CreateDivision";
                error.ErrorStackTrace = e.StackTrace;
                return RedirectToAction("Index", "AllErrors");
            }
            return RedirectToAction("DivisionIndex", divDeptSect);
        }

        public IActionResult DeleteDivision(int id)
        {
            try
            {
                var deletedDivision = _division.DeleteDivision(id);
                if (deletedDivision > 0)
                {
                    return RedirectToAction("DivisionIndex");
                }
            }
            catch(Exception e)
            {
                var error = new Errors();
                error.ErrorMessage = e.Message;
                error.ErrorEvent = "CreateDivision";
                error.ErrorStackTrace = e.StackTrace;
                return RedirectToAction("Index", "AllErrors");
            }
            return RedirectToAction("DivisionIndex");
        }

    }

}
