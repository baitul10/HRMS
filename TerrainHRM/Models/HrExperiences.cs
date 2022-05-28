using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TerrainHRM.Models
{
    public class HrExperiences
    {
        public HrExperiences()
        {

        }

        public int ExperienceId { get; set; }
        public int ApplicantId { get; set; }
        public virtual HrApplicant HrApplicant { get; private set; }

        public string CompanyName { get; set; }
        public string Designation { get; set; }
        public int YearsWorked { get; set; }
    }
}
