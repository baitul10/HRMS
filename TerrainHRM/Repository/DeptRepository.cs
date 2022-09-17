using System;
using System.Collections.Generic;
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

        public List<DeptMst> DeptListByDivision(int divmId)
        {
            var deptList = _context.Departments
                .Where(x => x.DeptDivmId == divmId)
                .OrderBy(x => x.DeptOrder)
                .ToList();
            if (deptList.Count==0)
            {
                deptList.Add(new DeptMst
                {
                    Id = 0
                });
            }
            return deptList;
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
