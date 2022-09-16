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
    public class HrmInitialOldController : Controller
    {
        private readonly IDesigRepository _desig;
        private readonly IDivisionRepository _division;
        private readonly IDeptRepository _dept;
        private readonly ISectRepository _sect;

        public HrmInitialOldController(IDesigRepository desig, IDivisionRepository division, IDeptRepository dept, ISectRepository sect)
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
            if (result <= 0)
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
                var divisions = _division.GetDivisions();
                if (divisions.Count == 0)
                {
                    divisions.Add(new DivisionMst());
                }
                divisions.OrderBy(d => d.DivmId);

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
                if (departments.Count == 0)
                {
                    departments.Add(new DeptMst());
                }
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

                if (sections.Count == 0)
                {
                    sections.Add(new SectionMst());
                }

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
                    if (department.Id > 0)
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
                if (deptListToInsert.Count > 0)
                {
                    var insertedDepts = _dept.CreatNewEntity(deptListToInsert);
                    divDeptSect.Departments.AddRange(insertedDepts);
                }
                if (deptListToUpdate.Count > 0)
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
            catch (Exception e)
            {
                var error = new Errors();
                error.ErrorMessage = e.Message;
                error.ErrorEvent = "CreateDivision";
                error.ErrorStackTrace = e.StackTrace;
                return RedirectToAction("Index", "AllErrors");
            }
            return RedirectToAction("DivisionIndex", divDeptSect);
        }

        //public IActionResult CreateDivDept(DivisionMst division, List<DeptMst> departments)
        //{

        //}

       //Newly added actions

        public IActionResult DivIndex()
        {

            var divDeptSect = new DivisionDto();
            try
            {
                var divisions = _division.GetDivisions();
                if (divisions.Count == 0)
                {
                    divisions.Add(new DivisionMst());
                }

                divisions.OrderBy(d => d.DivmId);

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
                if (departments.Count == 0)
                {
                    departments.Add(new DeptMst());
                }
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


            return View("DivIndex", divDeptSect);
        }
        private DivisionMst CreateDivision(DivisionMst division)
        {
            var div = _division.CreateDivision(division);
            return div;
        }

        private int CreateDepartment(List<DeptMst> departments, DivisionMst division)
        {
            var deptId = DbConnectionHelper.GetGeneratedPK("DEPT_MST", "DEPT_ID") + 1;
            var newList = new List<DeptMst>();
            foreach (var department in departments)
            {
                department.Id = deptId;
                department.DeptDivmId = division.DivmId;
                newList.Add(department);
                ++deptId;
            }
            if (newList.Count > 0)
            {
                _dept.CreatNewEntity(newList);
                return 1;
            }
            else return 0;
        }

        [HttpPost]
        public IActionResult CreateDivisionDept(DivDto division, List<DeptDto> departments)
        {
            var div = CreateDivision(new DivisionMst() { 
                DivmId = division.DivmId,
                DivmCode = division.DivmCode,
                DivmName = division.DivmName,
                DivmNameBn = division.DivmNameBn
            });
            var deptList = new List<DeptMst>();
             foreach (var item in departments)
            {
                deptList.Add(new DeptMst() {
                    Id = item.Id,
                    DeptOrder = item.DeptOrder,
                    DeptName = item.DeptName,
                    DeptNameBn = item.DeptNameBn
                });

            }
            var result = CreateDepartment(deptList, div);
            if (result>0)
            {
                return View("DivIndex");
            }
            else
            {
                var error = new Errors();
                error.ErrorMessage = "Insert Error Occurred";
                error.ErrorEvent = "CreateDivisionDept Gettting Sections";
                error.ErrorStackTrace = "";
                return RedirectToAction("Index", "AllErrors", error);
            }
        }

        //Newly added actions
        [HttpPost]
        public List<DeptMst> CreateDepartment(int id, List<DeptMst> prmDepartments)
        {
            var departments = new List<DeptMst>();
            var deptListToInsert = new List<DeptMst>();
            var deptListToUpdate = new List<DeptMst>();
            var deptId = DbConnectionHelper.GetGeneratedPK("DEPT_MST", "DEPT_ID") + 1;
            foreach (var department in prmDepartments)
            {
                if (department.Id > 0)
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
            if (deptListToInsert.Count > 0)
            {
                var insertedDepts = _dept.CreatNewEntity(deptListToInsert);
                departments.AddRange(insertedDepts);
            }
            if (deptListToUpdate.Count > 0)
            {
                var updatedDepts = _dept.UpdateEntity(deptListToUpdate);
                departments.AddRange(updatedDepts);
            }
            return departments;
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
            catch (Exception e)
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
