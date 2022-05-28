using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TerrainHRM.Helper;
using TerrainHRM.Interfaces;
using TerrainHRM.Models;

namespace TerrainHRM.Controllers
{
    public class ApplicantController : Controller
    {
        private readonly IApplicantRepository _applicant;

        public ApplicantController(IApplicantRepository applicant)
        {
            _applicant = applicant;
        }
        public IActionResult Index()
        {
            List<HrApplicant> applicants = _applicant.GetAllApplicants();
            return View(applicants);
        }

        public IActionResult ApplicantDetails(int id)
        {
            HrApplicant applicant = _applicant.GetApplicant(id);
            return View(applicant);            
        }

        [HttpGet]
        public IActionResult CreateApplicant()
        {
            HrApplicant applicant = new HrApplicant();
            applicant.HrExperiences.Add(new HrExperiences { ExperienceId = 1 });
            applicant.HrExperiences.Add(new HrExperiences { ExperienceId = 2 });
            applicant.HrExperiences.Add(new HrExperiences { ExperienceId = 3 });
            return View(applicant);
        }

        [HttpPost]
        public IActionResult CreateApplicant(HrApplicant applicant)
        {
            applicant.ApplicantId = DbConnectionHelper.GetMaxPKValue("HR_APPLICANT", "APPLICANT_ID")+1;
            var expId = DbConnectionHelper.GetMaxPKValue("HR_EXPERIENCE", "EXPERIENCE_ID") + 1;

            foreach (var item in applicant.HrExperiences)
            {
                item.ExperienceId = expId;
                expId++;
            }

            var result = _applicant.InsertApplicant(applicant);
            if (result==0)
            {
                return View(applicant);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
