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
        private readonly IDivisionMstRepository _division;
        private readonly IDeptRepository _dept;
        public HrmInitialController(IDivisionMstRepository division, IDeptRepository dept)
        {
            _division = division;
            _dept = dept;
        }
        public IActionResult Index()
        {
            var divisionList = _division.GetDivisionList();
            return View("Division",divisionList);
        }


        [HttpGet]
        public IActionResult CreateUpdateDivision(int id = 0)
        {
            var divDto = new DivisionMstDto();
            if (id>0)
            {
                var div = _division.GetDivisionById(id);
                divDto.Division = div;

                var deptList = _dept.GetListEntity().Where(d => d.DeptDivmId == id)
                    .OrderBy(x=>x.DeptOrder).ToList();
                divDto.Departments.AddRange(deptList);
            }
            else
            {
                var div = new DivisionMst();
                var dept = new DeptMst()
                {
                    Id = 0
                };
                divDto.Division = div;
                divDto.Departments.Add(dept);
            }

            return View(divDto);
        }

        [HttpPost]
        public IActionResult CreateUpdateDivision(DivisionMstDto divisionMstDto)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var resutl = _division.CreateUpdateDivision(divisionMstDto.Division);
                    if (resutl>0)
                    {
                        var deptId = DbConnectionHelper.GetGeneratedPK("DEPT_MST", "DEPT_ID") + 1;
                        var insertDeptList = new List<DeptMst>();
                        var updateDeptList = new List<DeptMst>();
                        foreach (var department in divisionMstDto.Departments)
                        {
                            if (department.Id == 0)
                            {
                                department.Id = deptId;
                                department.DeptDivmId = resutl;
                                ++deptId;
                                insertDeptList.Add(department);
                            }
                            else
                            {
                                department.DeptDivmId = resutl;
                                updateDeptList.Add(department);
                            }
                        }
                        if (insertDeptList.Count>0)
                        {
                            _dept.CreatNewEntity(insertDeptList);
                        }
                        if (updateDeptList.Count>0)
                        {
                            _dept.UpdateEntity(updateDeptList);
                        }
                    }
                    
                    if (resutl>0)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        var error = new Errors();
                        error.ErrorMessage = "Data could not be inserted";
                        error.ErrorEvent = "CreateUpdateDivision in HrmInitial";
                        error.ErrorStackTrace = "";
                        return RedirectToAction("Index", "AllErrors", error);
                    }
                }
                catch(Exception e)
                {
                    var error = new Errors();
                    error.ErrorMessage = e.Message;
                    error.ErrorEvent = "CreateUpdateDivision in HrmInitial";
                    error.ErrorStackTrace = e.StackTrace;
                    error.ErrorInnerMessage = e.InnerException.Message;
                    return RedirectToAction("Index", "AllErrors", error);
                }
            }
            else
            {
                var error = new Errors();
                error.ErrorMessage = "Model State is not valid.";
                error.ErrorEvent = "CreateUpdateDivision in HrmInitial";
                error.ErrorStackTrace = "";
                return RedirectToAction("Index", "AllErrors", error);
            }

            
        }
    }
}
