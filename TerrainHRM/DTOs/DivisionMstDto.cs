using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TerrainHRM.Models;

namespace TerrainHRM.DTOs
{
    public class DivisionMstDto
    {
        public DivisionMst Division { get; set; } = new DivisionMst();
        public List<DeptMst> Departments { get; set; } = new List<DeptMst>();
        public List<SectionMst> Sections { get; set; } = new List<SectionMst>();
    }
}
