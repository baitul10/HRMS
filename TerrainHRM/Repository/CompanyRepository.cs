using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TerrainHRM.Data;
using TerrainHRM.DTOs;
using TerrainHRM.Helper;
using TerrainHRM.Interfaces;
using TerrainHRM.Models;

namespace TerrainHRM.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly TerrainHRMContext _context;
        private int _cimId;
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


        public HrCompanyDto GetCompanyWithOffices()
        {
            var company = GetCompanyById(1);

            var companyWithOffice = new HrCompanyDto()
            {
                CimId = company.CimId,
                CimName = company.CimName,
                CimDetails = company.CimDetails,
                CimMoto = company.CimMoto,
                CimShortName = company.CimShortName,
                FileName = company.CimFileName,
                FileMymeType = company.CimFileMimetype,
                FileUpdateDate = (DateTime)company.CimUpdateDate
            };

            var companyDtl = _context.CompanyInfoDtl.Where(x => x.CidCimId == 1).ToList();
            companyWithOffice.CompanyDtlList.AddRange(companyDtl);

            var offices = _context.CompanyOfficeAddress.ToList();
            companyWithOffice.CompanyOffices.AddRange(offices);

            return companyWithOffice;
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

        private CompanyInfoMst CreateUpdateCompany(CompanyInfoMst company)
        {
            var maxCimId = DbConnectionHelper.GetGeneratedPK("COMPANY_INFO_MST", "CIM_ID") + 1;
            if (company.CimId > 0)
            {
                //if (string.IsNullOrEmpty(company.CimFileName))
                //{
                //    //var companyOld = _context.CompanyInfoMst.Where(x => x.CimId == 1).FirstOrDefault();
                //    company.CimFileName = companyOld.CimFileName;
                //    company.CimFileMimetype = companyOld.CimFileMimetype;
                //    company.CimUpdateDate = companyOld.CimUpdateDate;
                //}

                _context.CompanyInfoMst.Update(company);
                _cimId = company.CimId;
            }
            else if (company.CimId == 0)
            {
                company.CimId = maxCimId;
                _context.CompanyInfoMst.Add(company);
                _cimId = maxCimId;
            }

            var result = _context.SaveChanges();
            if (result > 0)
            {
                return company;
            }
            else
            {
                company = new CompanyInfoMst();
                return company;
            }


            //return company;
        }

        private List<CompanyInfoDtl> CreateUpdateCompanyDtl(List<CompanyInfoDtl> companies)
        {
            var result = 0;
            var maxCidId = DbConnectionHelper.GetGeneratedPK("COMPANY_INFO_DTL", "CID_ID") + 1;
            var companyNewList = new List<CompanyInfoDtl>();
            if (companies.Count>0)
            {
                foreach (var company in companies)
                {
                    if (company.CidId>0)
                    {
                        company.CidCimId = _cimId;
                        _context.CompanyInfoDtl.Update(company);
                        companyNewList.Add(company);
                    } 
                    else if (company.CidId == 0)
                    {
                        company.CidId = maxCidId;
                        company.CidCimId = _cimId;
                        _context.CompanyInfoDtl.Add(company);
                        companyNewList.Add(company);
                        ++maxCidId;
                    }                    
                }
                result = _context.SaveChanges();
                
            }
            if (result>0)
            {
                return companyNewList;
            }
            else
            {
                companyNewList = new List<CompanyInfoDtl>()
                {
                    new CompanyInfoDtl
                    {
                        CidId = 0
                    }
                };
                return companyNewList;
            }
           
        }

        private List<CompanyOfficeAddress> CreateUpdateOffice(List<CompanyOfficeAddress> offices)
        {
            var result = 0;
            var officeList = new List<CompanyOfficeAddress>();
            var coaId = DbConnectionHelper.GetGeneratedPK("COMPANY_OFFICE_ADDRESS", "COA_ID") + 1;
            if (offices.Count>0)
            {
                foreach (var office in offices)
                {
                    if (office.CoaId>0)
                    {
                        _context.CompanyOfficeAddress.Update(office);
                        officeList.Add(office);
                    }
                    else if (office.CoaId == 0)
                    {
                        office.CoaId = coaId;
                        _context.CompanyOfficeAddress.Add(office);
                        officeList.Add(office);
                        ++coaId;
                    }
                }
                result = _context.SaveChanges();
            }
            if (result>0)
            {
                return officeList;
            }
            else
            {
                officeList = new List<CompanyOfficeAddress>()
                {
                    new CompanyOfficeAddress
                    {
                        CoaId = 0
                    }
                };
                return officeList;
            }
        }

        public HrCompanyDto CreateComapnyOffice(CompanyInfoMst company, List<CompanyInfoDtl> companiesDtl, List<CompanyOfficeAddress> offices)
        {
            bool isSuccess = false;
            var companyDto = new HrCompanyDto();
            //try
            //{
                if (!string.IsNullOrEmpty(company.CimName))
                {
                    var newCompany = CreateUpdateCompany(company);
                    isSuccess = true;

                    companyDto.CimName = newCompany.CimName;
                    companyDto.CimShortName = newCompany.CimShortName;
                    companyDto.CimDetails = newCompany.CimDetails;
                    companyDto.CimMoto = newCompany.CimMoto;
                    companyDto.FileName = newCompany.CimFileName;
                    companyDto.FileUpdateDate = newCompany.CimUpdateDate.HasValue ? (DateTime)newCompany.CimUpdateDate: DateTime.Now;
                    companyDto.FileMymeType = newCompany.CimFileMimetype;


                    var isCompanyDtlInserted = CreateUpdateCompanyDtl(companiesDtl);
                    companyDto.CompanyDtlList = isCompanyDtlInserted;
                    //result += isCompanyDtlInserted;
                    var isCompanyOfficeInserted = CreateUpdateOffice(offices);
                    companyDto.CompanyOffices = isCompanyOfficeInserted;
                    //result += isCompanyOfficeInserted;
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                    //throw new Exception("Unauthorized error occurred!");
                }
           // } 
        //catch(Exception e)
            //{
            //    var message = e.Message;
            //    isSuccess = false;
            //    //throw new Exception("Unauthorized error occurred!");
            //}
            if (isSuccess)
            {
                return companyDto;
            }
            else
            {
                companyDto = new HrCompanyDto()
                {
                    CompanyDtlList = new List<CompanyInfoDtl>() { new CompanyInfoDtl { CidId = 0 } },
                    CompanyOffices = new List<CompanyOfficeAddress>() { new CompanyOfficeAddress { CoaId = 0 } }
                };
                return companyDto;
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
