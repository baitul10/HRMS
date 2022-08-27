namespace TerrainHRM.Models
{
    public class SectionMst
    {
        public int Id { get; set; }
        public string SectName { get; set; }
        public int? SectType { get; set; }
        public int SectDeptId { get; set; }
        public int SectOrder { get; set; }
        public string SectNameBn { get; set; }
        public int? SectTdlId { get; set; }
    }
}
