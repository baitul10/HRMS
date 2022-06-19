using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace TerrainHRM.Models
{
    public partial class LdUser
    {
        public long LdId { get; set; }
        public DateTime LdLastmodified { get; set; }
        public long LdRecordversion { get; set; }
        public decimal LdDeleted { get; set; }
        public long LdTenantid { get; set; }
        public decimal LdEnabled { get; set; }
        public string LdUsername { get; set; }
        public string LdPassword { get; set; }
        public string LdPasswordmd4 { get; set; }
        public string LdName { get; set; }
        public string LdFirstname { get; set; }
        public string LdStreet { get; set; }
        public string LdPostalcode { get; set; }
        public string LdCity { get; set; }
        public string LdCountry { get; set; }
        public string LdState { get; set; }
        public string LdLanguage { get; set; }
        public string LdEmail { get; set; }
        public string LdTelephone { get; set; }
        public string LdTelephone2 { get; set; }
        public decimal LdType { get; set; }
        public DateTime? LdPasswordchanged { get; set; }
        public decimal LdPasswordexpires { get; set; }
        public decimal LdSource { get; set; }
        public long LdQuota { get; set; }
        public string LdCert { get; set; }
        public string LdCertsubject { get; set; }
        public string LdCertdigest { get; set; }
        public string LdKey { get; set; }
        public string LdKeydigest { get; set; }
        public decimal? LdWelcomescreen { get; set; }
        public string LdIpwhitelist { get; set; }
        public string LdIpblacklist { get; set; }
        public decimal LdPasswordexpired { get; set; }
    }
}
