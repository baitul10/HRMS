using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace TerrainHRM.Models
{
    public partial class HrApplicant
    {
        public HrApplicant()
        {
            HrExperience = new HashSet<HrExperience>();
        }

        public decimal ApplicantId { get; set; }
        public string ApplicantName { get; set; }
        public string Gender { get; set; }
        public decimal? Age { get; set; }
        public decimal? TotalExperiences { get; set; }

        public virtual ICollection<HrExperience> HrExperience { get; set; }
    }
}
