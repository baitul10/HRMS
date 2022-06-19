using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TerrainHRM.Models
{
    public class CompanyDtl
    {
        public int CidId { get; set; }
        [Required]
        [DisplayName("Company Name")]
        public string CidName { get; set; }
        [DisplayName("Short Name")]
        public string CidShortName { get; set; }
        [DisplayName("Address")]
        public string CidDetails { get; set; }
        [DisplayName("Moto")]
        public string CidMoto { get; set; }
        public int CimId { get; set; }

        public virtual CompanyMst CompanyMst { get; private set; }
    }
}
