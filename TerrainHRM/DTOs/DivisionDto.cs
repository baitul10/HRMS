using System.Collections.Generic;
using TerrainHRM.Models;

namespace TerrainHRM.DTOs
{
    public class DivisionDto
    {
        public List<DivisionMst> Division { get; set; } = new List<DivisionMst>();
        public List<DeptMst> Departments { get; set; } = new List<DeptMst>();
        public List<SectionMst> Sections { get; set; } = new List<SectionMst>();
    }
}
