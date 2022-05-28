using Microsoft.AspNetCore.Http;
using System;

namespace TerrainHRM.DTOs
{
    public class HrCompanyDto
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

        public IFormFile Logo { get; set; }
    }
}
