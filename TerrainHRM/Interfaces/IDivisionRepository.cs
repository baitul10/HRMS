using System.Collections.Generic;
using TerrainHRM.Models;

namespace TerrainHRM.Interfaces
{
    public interface IDivisionRepository
    {
        DivisionMst GetDivision(int id);
        List<DivisionMst> GetDivisions();
        List<DivisionMst> CreatUpdateDivision(List<DivisionMst> divisions);
        int DeleteDivision(int id);
    }
}
