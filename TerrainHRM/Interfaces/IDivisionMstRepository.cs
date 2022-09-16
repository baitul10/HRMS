using System.Collections.Generic;
using TerrainHRM.Models;

namespace TerrainHRM.Interfaces
{
    public interface IDivisionMstRepository
    {
        List<DivisionMst> GetDivisionList();
        DivisionMst GetDivisionById(int id);
        int CreateUpdateDivision(DivisionMst division);
        int DeleteDivision(int id);
    }
}
