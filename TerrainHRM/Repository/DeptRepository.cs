using System;
using System.Linq;
using TerrainHRM.Data;
using TerrainHRM.Interfaces;
using TerrainHRM.Models;

namespace TerrainHRM.Repository
{
    public class DeptRepository : GenericRepository<DeptMst>, IDeptRepository
    {
        private readonly TerrainHRMContext _context;
        public DeptRepository(TerrainHRMContext context)
            :base(context)
        {
            _context = context;
        }

        //public DeptMst GetDept(int id)
        //{
        //    var dept = _context.Departments.Where(x => x.Id == id).FirstOrDefault();
        //    if (dept==null)
        //    {
        //        throw new Exception("No Data Found.");
        //    }

        //    return dept;
        //}
    }
}
