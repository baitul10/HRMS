using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TerrainHRM.Data;
using TerrainHRM.Helper;
using TerrainHRM.Interfaces;
using TerrainHRM.Models;

namespace TerrainHRM.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly TerrainHRMContext _context;
        public CompanyRepository(TerrainHRMContext context)
        {
            _context = context;
        }

        public List<CompanyInfoMst> GetCompanyList()
        {
            var companyList = _context.CompanyInfoMst.ToList();
            return companyList;
        }

        public CompanyInfoMst GetCompanyById(int id)
        {
            var company = _context.CompanyInfoMst.Where(x => x.CimId == id).Include(x=>x.CompanyInfoDtl).FirstOrDefault();
            return company;
        }
        public int CreateNewCompany(CompanyInfoMst company, List<CompanyInfoDtl> companies)
        {
            try
            {
                var maxCimId = DbConnectionHelper.GetGeneratedPK("COMPANY_INFO_MST", "CIM_ID") + 1;
                var maxCidId = DbConnectionHelper.GetGeneratedPK("COMPANY_INFO_DTL", "CID_ID") + 1;
                company.CimId = maxCimId;
                _context.CompanyInfoMst.Add(company);
                foreach (var item in companies)
                {
                    item.CidCimId = maxCimId;
                    item.CidId = maxCidId;
                    _context.CompanyInfoDtl.Add(item);
                    //company.CompanyInfoDtl.Add(item);
                    ++maxCidId;
                }
                //_context.CompanyInfoMst.Add(company);
                var result = _context.SaveChanges();
                return result;
            }
            catch (System.Exception)
            {

                throw new System.Exception("Unauthorized Error Occurred.");
            }
            
        }

        public int DeleteCompany(int id)
        {
            var company = GetCompanyById(id);
            _context.CompanyInfoMst.Remove(company);
            var result = _context.SaveChanges();
            return result;
        }

        public int EditCompany(CompanyInfoMst company)
        {
            _context.CompanyInfoMst.Update(company);
            var result = _context.SaveChanges();
            return result;
        }


        public int CreateCompanyOffice(List<CompanyOfficeAddress> companyOffices)
        {

            var coaId = DbConnectionHelper.GetGeneratedPK("COMPANY_OFFICE_ADDRESS", "COA_ID") + 1;

            if (companyOffices.Count > 0)
            {
                foreach (var office in companyOffices)
                {
                    office.CoaId = coaId;
                    _context.CompanyOfficeAddress.Add(office);
                    ++coaId;
                }
                var result = _context.SaveChanges();
                return result;
            }
            else
            {
                return 0;
            }
        }
        public int UpdateCompanyOffice(List<CompanyOfficeAddress> companyOffices)
        {
            if (companyOffices.Count > 0)
            {
                foreach (var office in companyOffices)
                {
                    _context.CompanyOfficeAddress.Update(office);
                }
                var result = _context.SaveChanges();
                return result;
            }
            else {
                return 0;
            } 

        }
        public int DeleteCompanyOffice(int id)
        {
            var office = _context.CompanyOfficeAddress.Where(x => x.CoaId == id).FirstOrDefault();
            _context.CompanyOfficeAddress.Remove(office);
            var result = _context.SaveChanges();
            if (result==0)
            {
                return 0;
            }
            return result;
        }
    }
}
