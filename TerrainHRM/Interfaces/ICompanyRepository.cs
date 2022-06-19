using System.Collections.Generic;
using TerrainHRM.Models;

namespace TerrainHRM.Interfaces
{
    public interface ICompanyRepository
    {
        List<CompanyInfoMst> GetCompanyList();
        CompanyInfoMst GetCompanyById(int id);
        int CreateNewCompany(CompanyInfoMst company, List<CompanyInfoDtl> companies);
        int EditCompany(CompanyInfoMst company);
        int DeleteCompany(int id);

        int CreateCompanyOffice(List<CompanyOfficeAddress> companyOffices);
        int UpdateCompanyOffice(List<CompanyOfficeAddress> companyOffices);
        int DeleteCompanyOffice(int id);

    }
}
