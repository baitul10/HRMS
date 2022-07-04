using System.Collections.Generic;
using System.Linq;
using TerrainHRM.Data;
using TerrainHRM.Interfaces;
using TerrainHRM.Models;

namespace TerrainHRM.Repository
{
    public class DesigRepository : IDesigRepository
    {
        private readonly TerrainHRMContext _context;
        public DesigRepository(TerrainHRMContext context)
        {
            _context = context;
        }
        public List<DesigMst> GetDesigList()
        {
            var deisgList = _context.DesigMsts.ToList();
            return deisgList;
        }

        public DesigMst GetDesigById(int id)
        {
            var desig = _context.DesigMsts.Where(d => d.DesigId == id).FirstOrDefault();
            return desig;
        }

        public List<DesigMst> CreateUpdateDesig(List<DesigMst> desig)
        {
            var desigList = new List<DesigMst>();
            if (desig.Count>0)
            {
                foreach (var item in desig)
                {
                    if (item.DesigId>0)
                    {
                        _context.DesigMsts.Update(item);
                    }
                }
            }
            return desigList;
        }

        public int DeleteDesig(int id)
        {
            var singleDesig = _context.DesigMsts.Where(x => x.DesigId == id).FirstOrDefault();
            _context.DesigMsts.Remove(singleDesig);
            var result = _context.SaveChanges();
            return result;
        }
    }
}
