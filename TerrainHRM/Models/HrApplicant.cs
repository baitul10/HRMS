using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TerrainHRM.Models
{
    public class HrApplicant
    {
        public int ApplicantId { get; set; }
        [Required]
        [DisplayName("Applicant Name")]
        public string Name { get; set; }

        public string Gender { get; set; }
        public int Age { get; set; }
        [Range(1, 25, ErrorMessage = "Sorry! we have no valid position for your experiences")]
        public int TotalExperiences { get; set; }

        public virtual List<HrExperiences> HrExperiences { get; set; } = new List<HrExperiences>();
    }
}
