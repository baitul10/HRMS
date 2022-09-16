using System.Collections.Generic;
using System.Linq;
using TerrainHRM.Data;
using TerrainHRM.Helper;
using TerrainHRM.Interfaces;
using TerrainHRM.Models;

namespace TerrainHRM.Repository
{
    public class DivisionMstRepository : IDivisionMstRepository
    {
        private readonly TerrainHRMContext _context;
        public DivisionMstRepository(TerrainHRMContext context)
        {
            _context = context;
        }
        public int CreateUpdateDivision(DivisionMst division)
        {
            var result = 0;
            if (division.DivmId>0)
            {
                _context.Divisions.Update(division);
                result = _context.SaveChanges();
                if (result>0)
                {
                    result = division.DivmId;
                }
            }
            else
            {
                var divisionId = DbConnectionHelper.GetGeneratedPK("DIVISION_MST", "DIVM_ID") + 1;
                division.DivmId = divisionId;
                _context.Divisions.Add(division);
                result = _context.SaveChanges();
                if (result>0)
                {
                    result = divisionId;
                }
            }

            return result;
        }

        public int DeleteDivision(int id)
        {
            var division = _context.Divisions.Where(d => d.DivmId == id).FirstOrDefault();
            _context.Remove(division);
            var result = _context.SaveChanges();
            return result;
        }

        public DivisionMst GetDivisionById(int id)
        {
            var division = _context.Divisions.Where(d => d.DivmId == id).FirstOrDefault();

            return division;
        }

        public List<DivisionMst> GetDivisionList()
        {
            var divisionList = _context.Divisions.ToList();
            return divisionList;
        }
    }
}
