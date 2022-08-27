using System.Collections.Generic;

namespace TerrainHRM.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        List<T> GetListEntity();
        T GetSingleEntity(int id);
        List<T> CreatNewEntity(List<T> Tlist);
        List<T> UpdateEntity(List<T> Tlist);
        int DeleteEntity(T entity);
    }
}
