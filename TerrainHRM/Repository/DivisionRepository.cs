using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TerrainHRM.Data;
using TerrainHRM.Helper;
using TerrainHRM.Interfaces;
using TerrainHRM.Models;

namespace TerrainHRM.Repository
{
    public class DivisionRepository : IDivisionRepository
    {
        private readonly TerrainHRMContext _context;
        public DivisionRepository(TerrainHRMContext context)
        {
            _context = context;
        }
        public List<DivisionMst> CreatUpdateDivision(List<DivisionMst> divisions)
        {
            var divisionList = new List<DivisionMst>();

            if (divisions.Count>0)
            {
                foreach (var division in divisions)
                {
                    var maxDivmId = DbConnectionHelper.GetGeneratedPK("DESIG_MST", "DESIG_ID") + 1;
                    if (division.DivmId>0)
                    {
                        _context.Divisions.Update(division);
                        _context.SaveChanges();
                        divisionList.Add(division);
                    }
                    else
                    {
                        division.DivmId = maxDivmId;
                        _context.Divisions.Add(division);
                        _context.SaveChanges();
                        divisionList.Add(division);
                    }
                }
            }

            return divisionList;
        }

        public int DeleteDivision(int id)
        {
            var division = GetDivision(id);
            _context.Divisions.Remove(division);
            var result = _context.SaveChanges();
            return result;
        }

        public DivisionMst GetDivision(int id)
        {
            var division = _context.Divisions.Where(x => x.DivmId == id).FirstOrDefault();
            return division;
        }

        public List<DivisionMst> GetDivisions()
        {
            var divisions = _context.Divisions.ToList();
            return divisions;
        }
    }
}
