using System;
using System.Collections.Generic;
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

        public List<SectionMst> SectionListByDept(int deptId)
        {
            var sectionList = _context.Sections
                .Where(x => x.SectDeptId == deptId)
                .OrderBy(x=>x.SectOrder)
                .ToList();
            if (sectionList.Count==0)
            {
                sectionList.Add(new SectionMst()
                {
                    Id = 0
                });
            }
            return sectionList;
        }
    }
}
