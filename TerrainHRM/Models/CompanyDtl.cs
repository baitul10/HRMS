namespace TerrainHRM.Models
{
    public class CompanyDtl
    {
        public int CidId { get; set; }
        public string CidName { get; set; }
        public string CidShortName { get; set; }
        public string CidDetails { get; set; }
        public string CidMoto { get; set; }
        public int CimId { get; set; }
        public HrCompany HrCompany { get; set; }
    }
}
