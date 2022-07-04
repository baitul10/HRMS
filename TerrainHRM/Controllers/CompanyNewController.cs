using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TerrainHRM.DTOs;
using TerrainHRM.Helper;
using TerrainHRM.Interfaces;
using TerrainHRM.Models;

namespace TerrainHRM.Controllers
{
    public class CompanyNewController : Controller
    {
        private readonly ICompanyRepository _company;
        public CompanyNewController(ICompanyRepository company)
        {
            _company = company;
        }

        public IActionResult Index()
        {
            var companyList = _company.GetCompanyList();
            return View(companyList);
        }

        public IActionResult CompanyDetails(int id)
        {
            var company = _company.GetCompanyById(id);


            //var company = companies.Where(x => x.CimId == 1).FirstOrDefault();
            if (company != null)
            {
                ViewBag.ImagePath = company.CimFileName;
            }
            else
            {
                ViewBag.ImagePath = "";
            }
            return View(company);
        }

        public IActionResult CreateNewCompany()
        {
            var company = new HrCompanyDto();

            company.CompanyDtlList = new List<CompanyInfoDtl>() { new CompanyInfoDtl { CidId = 1 } };
            company.CompanyOffices = new List<CompanyOfficeAddress>()
            {
                new CompanyOfficeAddress{CoaId = 1}
            };

            return View(company);
        }

        [HttpPost]
        public IActionResult CreateNewCompany(CompanyInfoMst company, List<CompanyInfoDtl> companyDtls)
        {
            if (company.CimName == null)
            {
                var companyNew = new HrCompanyDto();

                companyNew.CompanyDtlList = new List<CompanyInfoDtl>() { new CompanyInfoDtl { CidId = 1 } };
                ViewBag.errorMessage = "Company Name or Company details cannot be null";
                return View(companyNew);
            }

            if (ModelState.IsValid)
            {

                //company.CimId = maxCimId;
                var result = _company.CreateNewCompany(company, companyDtls);
                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View();
                }

            }
            else
            {
                return View();
            }

            //return RedirectToAction(nameof(Index));
        }

        public IActionResult EditCompanyInfo(CompanyInfoMst company)
        {
            if (ModelState.IsValid)
            {
                var result = _company.EditCompany(company);
                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        public IActionResult DeleteCompany(int id)
        {
            var result = _company.DeleteCompany(id);
            if (result > 0)
            {
                ViewBag.DeleteAlert("Data Deleted Successfully!");
            }
            return RedirectToAction(nameof(Index));
        }


        public int InsertUpdateCompanyOffice(List<CompanyOfficeAddress> companyOffices)
        {
            List<CompanyOfficeAddress> officeToInsert = new List<CompanyOfficeAddress>();
            List<CompanyOfficeAddress> officeToUpdate = new List<CompanyOfficeAddress>();
            foreach (var office in companyOffices)
            {
                if (office.CoaId > 0)
                {
                    officeToUpdate.Add(office);
                }
                else
                {
                    officeToInsert.Add(office);
                }
            }
            int result = 0;
            if (officeToInsert.Count > 0)
            {
                var insertResult = _company.CreateCompanyOffice(officeToInsert);
                result = result + insertResult;
            }

            if (officeToUpdate.Count > 0)
            {
                var updateResult = _company.UpdateCompanyOffice(officeToUpdate);
                result = result + updateResult
;
            }

            return result;
        }


        public int DeleteCompanyOffice(int id)
        {
            var officeDeleted = _company.DeleteCompanyOffice(id);
            return officeDeleted;
        }

        [HttpGet]
        public IActionResult CreateUpdateCompanyInfo()
        {
            var company = _company.GetCompanyWithOffices();
            if (company.CimId==1)
            {
                return View(company);
            }
            else
            {
                company = new HrCompanyDto();
                company.CompanyDtlList = new List<CompanyInfoDtl>()
                    {
                        new CompanyInfoDtl(){CidId = 0}
                    };

                        company.CompanyOffices = new List<CompanyOfficeAddress>()
                    {
                        new CompanyOfficeAddress(){CoaId = 0}
                    };
            }

            return View(company);
        }

        [HttpPost]
        public IActionResult CreateUpdateCompanyInfo(CompanyInfoMst company, List<CompanyInfoDtl> companyDtl, List<CompanyOfficeAddress> offices)
        {
            var companyDto = new HrCompanyDto();
            //try
            //{
                companyDto = _company.CreateComapnyOffice(company, companyDtl, offices);
            //}
           // catch(Exception e)
           // {
             //   var message = e.Message;
              //  throw new Exception("Unauthorized Error Occurred.");
            //}
            return View(companyDto);
        }
    }
}
