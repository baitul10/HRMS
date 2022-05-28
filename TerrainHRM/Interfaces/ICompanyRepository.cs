using System.Collections.Generic;
using TerrainHRM.Models;

namespace TerrainHRM.Interfaces
{
    public interface ICompanyRepository
    {
        int CreateCompany(HrCompany company);
        HrCompany GetCompany();
        List<Employee> GetEmployees();
        int EditCompanyInfo(HrCompany company);
    }
}
