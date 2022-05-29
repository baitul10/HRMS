using System;
using System.Collections.Generic;

namespace TerrainHRM.Models
{
    public class HrCompany
    {
        public int CimId { get; set; }
        public string CimName { get; set; }
        public string CimDetails { get; set; }
        public string CimMoto { get; set; }
        public string CimShortName { get; set; }
        public int MultiCompanyFlag { get; set; }
        public string FileName { get; set; }
        public DateTime FileUpdateDate { get; set; }
        public string FileMymeType { get; set; }
        public string FileCharacter { get; set; }
        public byte[] CimLogoApps { get; set; }

        public virtual List<CompanyDtl> CompanyDtlList { get; set; } = new List<CompanyDtl>();
    }
}
