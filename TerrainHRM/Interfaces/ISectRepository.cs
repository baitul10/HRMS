using System.Collections.Generic;
using TerrainHRM.Models;

namespace TerrainHRM.Interfaces
{
    public interface ISectRepository : IGenericRepository<SectionMst>
    {
        List<SectionMst> SectionListByDept(int deptId);
        SectionMst GetSection(int id);
    }
}
