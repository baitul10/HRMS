using System.Collections.Generic;
using TerrainHRM.Models;

namespace TerrainHRM.Interfaces
{
    public interface ICompanyRepositoryOld
    {
        int CreateCompany(CompanyMst company);
        CompanyMst GetCompany();
        List<Employee> GetEmployees();
        int EditCompanyInfo(CompanyMst company);
    }
}
