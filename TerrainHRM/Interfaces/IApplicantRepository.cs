using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TerrainHRM.Models;

namespace TerrainHRM.Interfaces
{
    public interface IApplicantRepository
    {
        HrApplicantOld GetApplicant(int id);
        List<HrApplicantOld> GetAllApplicants();
        int InsertApplicant(HrApplicantOld applicant);

    }
}
