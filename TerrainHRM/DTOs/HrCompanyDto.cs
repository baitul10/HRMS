﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TerrainHRM.Models;

namespace TerrainHRM.DTOs
{
    public class HrCompanyDto
    {
        public int CimId { get; set; }
        [Required]
        [DisplayName("Company Name")]
        public string CimName { get; set; }
        [DisplayName("Address")]
        public string CimDetails { get; set; }
        [DisplayName("Moto")]
        public string CimMoto { get; set; }
        [DisplayName("Short Name")]
        public string CimShortName { get; set; }
        public int MultiCompanyFlag { get; set; }
        public string FileName { get; set; }
        public DateTime FileUpdateDate { get; set; }
        public string FileMymeType { get; set; }
        public string FileCharacter { get; set; }
        public byte[] CimLogoApps { get; set; }

        public IFormFile Logo { get; set; }

        public virtual List<CompanyInfoDtl> CompanyDtlList { get; set; } = new List<CompanyInfoDtl>();
        public virtual List<CompanyOfficeAddress> CompanyOffices { get; set; } = new List<CompanyOfficeAddress>();
    }
}
