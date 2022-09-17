using System.Collections.Generic;
using TerrainHRM.Models;

namespace TerrainHRM.Interfaces
{
    public interface IDeptRepository : IGenericRepository<DeptMst>
    {
        List<DeptMst> DeptListByDivision(int divmId);
        
    }
}
