using System;

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
    }
}


//SELECT CIM_ID, CIM_NAME, CIM_DETAILS,
//   CIM_LOGO_APPS, CIM_MOTO, CIM_SHORT_NAME,
//   CIM_MULTI_COMPANY_FLAG, CIM_MULTI_ADDRESS_FLAG, CIM_LOGO,
//   CIM_FILE_NAME, CIM_UPDATE_DATE, CIM_FILE_MIMETYPE,
//   CIM_FILE_CHARACTER
//FROM DU.COMPANY_INFO_MST