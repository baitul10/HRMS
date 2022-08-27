namespace TerrainHRM.Models
{
    public class DeptMst
    {
        public int Id { get; set; }
        public string DeptName { get; set; }
        public int? DeptType { get; set; }
        public int DeptDivmId { get; set; }
        public int DeptOrder { get; set; }
        public int? DeptCliHrmFlag { get; set; }
        public string DeptCode { get; set; }
        public string DeptNameBn { get; set; }
        public int? DeptTdlId { get; set; }
    }
}
