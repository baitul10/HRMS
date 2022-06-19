using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace TerrainHRM.Models
{
    public partial class CompanyInfoMst
    {
        public CompanyInfoMst()
        {
            CompanyInfoDtl = new HashSet<CompanyInfoDtl>();
        }

        public int CimId { get; set; }
        public string CimName { get; set; }
        public string CimDetails { get; set; }
        public byte[] CimLogoApps { get; set; }
        public string CimMoto { get; set; }
        public string CimShortName { get; set; }
        public byte? CimMultiCompanyFlag { get; set; }
        public bool? CimMultiAddressFlag { get; set; }
        public byte[] CimLogo { get; set; }
        public string CimFileName { get; set; }
        public DateTime? CimUpdateDate { get; set; }
        public string CimFileMimetype { get; set; }
        public string CimFileCharacter { get; set; }

        public virtual ICollection<CompanyInfoDtl> CompanyInfoDtl { get; set; }
    }
}
