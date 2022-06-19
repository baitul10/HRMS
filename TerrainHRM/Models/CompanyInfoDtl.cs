using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace TerrainHRM.Models
{
    public partial class CompanyInfoDtl
    {
        public int CidId { get; set; }
        public string CidName { get; set; }
        public string CidDetails { get; set; }
        public byte[] CidLogo { get; set; }
        public string CidMoto { get; set; }
        public int? CidCimId { get; set; }
        public string CidShortName { get; set; }

        public virtual CompanyInfoMst CidCim { get; set; }
    }
}
