using System.Collections.Generic;
using System.Linq;
using TerrainHRM.Data;
using TerrainHRM.Interfaces;

namespace TerrainHRM.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly TerrainHRMContext _context;
        public GenericRepository(TerrainHRMContext context)
        {
            _context = context;
        }
        public List<T> CreatNewEntity(List<T> Tlist)
        {
            foreach (var entity in Tlist)
            {
                _context.Add(entity);
            }
            _context.SaveChanges();
            return Tlist;
        }

        public int DeleteEntity(T enity)
        {
            _context.Remove(enity);
            var result = _context.SaveChanges();
            return result;
        }

        public List<T> GetListEntity()
        {
            var entityList = _context.Set<T>().ToList();
            return entityList;
        }

        public T GetSingleEntity(int id)
        {
            var entity = _context.Set<T>().Find(id);
            return entity;
        }

        public List<T> UpdateEntity(List<T> Tlist)
        {
            foreach (var entity in Tlist)
            {
                _context.Update(entity);
            }
            _context.SaveChanges();
            return Tlist;
        }
    }
}
