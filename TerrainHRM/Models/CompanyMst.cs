using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TerrainHRM.Models
{
    public class CompanyMst
    {
        public int CimId { get; set; }
        [Required]
        [DisplayName("Company Name")]
        public string CimName { get; set; }
        [DisplayName("Address")]
        public string CimDetails { get; set; }
        [DisplayName("Moto")]
        public string CimMoto { get; set; }
        [Required]
        [DisplayName("Short Name")]
        public string CimShortName { get; set; }
        public int MultiCompanyFlag { get; set; }
        public string FileName { get; set; }
        public DateTime FileUpdateDate { get; set; }
        public string FileMimeType { get; set; }
        public string FileCharacter { get; set; }
        public byte[] CimLogoApps { get; set; }

        public IFormFile Logo { get; set; }

        public virtual List<CompanyDtl> CompanyDtlList { get; set; } = new List<CompanyDtl>();
    }
}
