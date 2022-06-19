using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace TerrainHRM.Models
{
    public partial class LdMenu
    {
        public long LdId { get; set; }
        public DateTime LdLastmodified { get; set; }
        public long LdRecordversion { get; set; }
        public decimal LdDeleted { get; set; }
        public long LdTenantid { get; set; }
        public string LdText { get; set; }
        public long LdParentid { get; set; }
        public long? LdSecurityref { get; set; }
        public string LdIcon { get; set; }
        public decimal LdType { get; set; }
        public string LdDescription { get; set; }
        public decimal LdPosition { get; set; }
    }
}
