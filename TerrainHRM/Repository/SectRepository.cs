using System;
using System.Linq;
using TerrainHRM.Data;
using TerrainHRM.Interfaces;
using TerrainHRM.Models;

namespace TerrainHRM.Repository
{
    public class SectRepository : GenericRepository<SectionMst>, ISectRepository
    {
        private TerrainHRMContext _context;
        public SectRepository(TerrainHRMContext context)
            :base(context)
        {
            _context = context;
        }
        public SectionMst GetSection(int id)
        {
            var section = _context.Sections.Where(x => x.Id == id).FirstOrDefault();
            if (section==null)
            {
                throw new Exception("No data found");
            }
            return section;
        }
    }
}
