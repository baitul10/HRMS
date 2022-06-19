using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TerrainHRM.Models
{
    public class CompanyOfficeAddress
    {
        public int CoaId { get; set; }
        public string CoaName { get; set; }
        public string CoaAddress { get; set; }
        public string CoaShortName { get; set; }
        public string CoaShortCode { get; set; }
        public int CoaUseTypeFlag { get; set; }
    }
}


//COA_ID NUMBER(20,0) NOT NULL ENABLE,
//	COA_NAME VARCHAR2(100), 
//	COA_ADDRESS VARCHAR2(1000), 
//	COA_SHORT_NAME VARCHAR2(20), 
//	COA_SHORT_CODE VARCHAR2(20), 
//	COA_USE_TYPE_FLAG NUMBER(2,0) DEFAULT 0, 