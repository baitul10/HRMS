using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TerrainHRM.Models;

namespace TerrainHRM.Interfaces
{
    public interface IApplicantRepository
    {
        HrApplicant GetApplicant(int id);
        List<HrApplicant> GetAllApplicants();
        int InsertApplicant(HrApplicant applicant);

    }
}
