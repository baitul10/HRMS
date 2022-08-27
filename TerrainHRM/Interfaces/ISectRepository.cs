using TerrainHRM.Models;

namespace TerrainHRM.Interfaces
{
    public interface ISectRepository : IGenericRepository<SectionMst>
    {
        SectionMst GetSection(int id);
    }
}
