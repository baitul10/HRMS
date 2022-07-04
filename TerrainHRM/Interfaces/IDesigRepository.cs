using System.Collections.Generic;
using TerrainHRM.Models;

namespace TerrainHRM.Interfaces
{
    public interface IDesigRepository
    {
        List<DesigMst> GetDesigList();
        DesigMst GetDesigById(int id);
        List<DesigMst> CreateUpdateDesig(List<DesigMst> desig);
        int DeleteDesig(int id);
    }
}
