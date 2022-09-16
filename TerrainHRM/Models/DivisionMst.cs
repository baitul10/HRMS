using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TerrainHRM.Models
{
    public class DivisionMst
    {
        [Required]
        public int DivmId { get; set; }

        [Required]
        [DisplayName("Division Name")]
        public string DivmName { get; set; }

        [DisplayName("Short Code")]
        public string DivmCode { get; set; }

        [DisplayName("Bangla Name")]
        public string DivmNameBn { get; set; }
        public int? DivmOpdFlag { get; set; }
    }
}
