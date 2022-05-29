using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TerrainHRM.DTOs;
using TerrainHRM.Interfaces;
using TerrainHRM.Models;

namespace TerrainHRM.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompanyRepository _company;
        private readonly IWebHostEnvironment _env;
        private readonly string _rootDir;
        public CompanyController(ICompanyRepository company, IWebHostEnvironment env)
        {
            _company = company;
            _env = env;
            _rootDir = Path.Combine(_env.WebRootPath, "images");
        }
        [HttpGet]
        public IActionResult Index()
        {
            var company = _company.GetCompany();
            //var company = companies.Where(x => x.CimId == 1).FirstOrDefault();
            if (company!=null)
            {
                ViewBag.ImagePath = company.FileName;
            }else
            {
                ViewBag.ImagePath = "";
            }
            //Path.Combine(_env.WebRootPath,"images", company.FileName);
            //var employees = _company.GetEmployees();
            return View(company);
        }

        [HttpGet]
        public IActionResult CreateCompany()
        {
            var company = new HrCompany();
            return View();
        }

        [HttpPost]
        public IActionResult CreateCompany(int id, string compnayName, string details, string moto, string shortname, int multiCompanyFlag , IFormFile logo )
        {
            var fileName = Path.GetFileName(logo.FileName);
            var fileExt = Path.GetExtension(logo.FileName);
            //var rootDir = Path.Combine(_env.WebRootPath, "images");
            if (!Directory.Exists(_rootDir))
            {
                Directory.CreateDirectory(_rootDir);
            }
            var rootPath = Path.Combine(_rootDir, fileName);

            var company = new HrCompany
            {
                CimId = id,
                CimName = compnayName,
                CimDetails = details,
                CimMoto = moto,
                CimShortName = shortname,
                MultiCompanyFlag = multiCompanyFlag,
                FileName = fileName,
                FileUpdateDate = DateTime.Now,
                FileMymeType = fileExt,
                FileCharacter = null
            };

            if (logo.Length>0)
            {
                using (var stream = new FileStream(rootPath, FileMode.Create))//File.Create(filePath, fileSize))
                {
                    logo.CopyTo(stream);
                }
            }

            var result = _company.CreateCompany(company);
            if (result==0)
            {
                return View();
            }
            return RedirectToAction(nameof(Index));
        }


        public IActionResult EditCompany()
        {
            var company = _company.GetCompany();
            var hrCompany = new HrCompanyDto()
            {
                CimId = company.CimId,
                CimName = company.CimName,
                CimDetails = company.CimDetails,
                CimShortName = company.CimShortName,
                CimMoto = company.CimMoto,
                FileName = company.FileName,
                FileMymeType = company.FileMymeType
            };

            hrCompany.CompanyDtlList.Add(new CompanyDtl { CidId = 1 });
            hrCompany.CompanyDtlList.Add(new CompanyDtl { CidId = 2 });
            hrCompany.CompanyDtlList.Add(new CompanyDtl { CidId = 3 });

            return View(hrCompany);
        }

        [HttpPost]
        public IActionResult EditCompany(HrCompanyDto company)
        {
            if (company!=null)
            {
                var fileName = Path.GetFileName(company.Logo.FileName);
                var fileExt = Path.GetExtension(company.Logo.FileName);
                if (company.Logo.Length > 0)
                {
                    var filePath = Path.Combine(_rootDir, company.FileName);
                    FileInfo fileInfo = new FileInfo(filePath);
                    if (fileInfo.Exists)
                    {
                        fileInfo.Delete();
                    }

                    //var rootDir = Path.Combine(_env.WebRootPath, "images");
                    if (!Directory.Exists(_rootDir))
                    {
                        Directory.CreateDirectory(_rootDir);
                    }
                    var rootPath = Path.Combine(_rootDir, fileName);


                    if (company.Logo.Length > 0)
                    {
                        using (var stream = new FileStream(rootPath, FileMode.Create))//File.Create(filePath, fileSize))
                        {
                            company.Logo.CopyTo(stream);
                        }
                    }

                }

                company.FileName = fileName;
                company.FileMymeType = fileExt;

                HrCompany hrCompany = new HrCompany()
                {
                    CimId = company.CimId,
                    CimName = company.CimName,
                    CimDetails = company.CimDetails,
                    CimShortName = company.CimShortName,
                    CimMoto = company.CimMoto,
                    FileName = fileName,
                    FileMymeType = fileExt,
                    FileUpdateDate = DateTime.Now
                };

                var result = _company.EditCompanyInfo(hrCompany);
                if (result==1)
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
    }
}
