using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using TerrainHRM.Helper;
using TerrainHRM.Interfaces;
using TerrainHRM.Models;

namespace TerrainHRM.Repository
{
    public class ApplicantRepository : IApplicantRepository
    {
        public List<HrApplicant> GetAllApplicants()
        {
            List<HrApplicant> applicants = new List<HrApplicant>();
            OracleConnection conn = DbConnectionHelper.GetOrclConnection();
            string query = "SELECT APPLICANT_ID, APPLICANT_NAME, GENDER, AGE, TOTAL_EXPERIENCES FROM HR_APPLICANT";
            OracleCommand cmd = new OracleCommand()
            {
                CommandText = query,
                CommandType = System.Data.CommandType.Text,
                Connection = conn
            };

            cmd.Connection.Open();
            var reader = cmd.ExecuteReader();

            if (reader.RowSize>0)
            {
                while (reader.Read())
                {
                    HrApplicant applicant = new HrApplicant();
                    applicant.ApplicantId = Convert.ToInt32(reader[0]);
                    applicant.Name = reader[1].ToString();
                    applicant.Gender = reader[2].ToString();
                    applicant.Age = Convert.ToInt32(reader[3]);
                    applicant.TotalExperiences = Convert.ToInt32(reader[4]);

                    applicants.Add(applicant);
                }
                reader.Close();
            }
            cmd.Connection.Close();
            cmd.Dispose();

            return applicants;
        }

        public HrApplicant GetApplicant(int id)
        {
            HrApplicant applicant = new HrApplicant();

            OracleConnection conn = DbConnectionHelper.GetOrclConnection();

            string query = "SELECT APPLICANT_ID, APPLICANT_NAME, GENDER, AGE, TOTAL_EXPERIENCES FROM HR_APPLICANT where APPLICANT_ID = " + id;

            OracleCommand cmd = new OracleCommand()
            {
                CommandText = query,
                CommandType = System.Data.CommandType.Text,
                Connection = conn
            };

            cmd.Connection.Open();
            var applicantReader = cmd.ExecuteReader();

            if (applicantReader.HasRows)
            {
                while(applicantReader.Read())
                {
                    applicant.ApplicantId = id;
                    applicant.Name = applicantReader[1].ToString();
                    applicant.Gender = applicantReader[2].ToString();
                    applicant.Age = Convert.ToInt32(applicantReader[3]);
                    applicant.TotalExperiences = Convert.ToInt32(applicantReader[4]);
                }

                applicantReader.Close();
            }

            //cmd.Connection.Close();
            //cmd.Dispose();


            string queryExp = "SELECT  EXPERIENCE_ID, APPLICANT_ID, COMPANY_NAME, DESIGNATION, YEARS_WORKED FROM HR_EXPERIENCE WHERE APPLICANT_ID = " + id;
            OracleCommand cmdExp = new OracleCommand()
            {
                CommandText = queryExp,
                CommandType = System.Data.CommandType.Text,
                Connection = conn
            };
            if (cmdExp.Connection.State == ConnectionState.Closed)
            {
                cmdExp.Connection.Open();
            }
            var readerExp = cmdExp.ExecuteReader();
            if (readerExp.HasRows)
            {
                while (readerExp.Read())
                {
                    HrExperiences experiences = new HrExperiences();
                    experiences.ExperienceId = Convert.ToInt32(readerExp[0]);
                    experiences.ApplicantId = Convert.ToInt32(readerExp[1]);
                    experiences.CompanyName = readerExp[2].ToString();
                    experiences.Designation = readerExp[3].ToString();
                    experiences.YearsWorked = Convert.ToInt32(readerExp[4]);

                    applicant.HrExperiences.Add(experiences);
                }
                readerExp.Close();
            }

            cmd.Connection.Close();
            cmd.Dispose();
            cmdExp.Connection.Close();
            cmdExp.Dispose();


            return applicant;
        }


        public int InsertApplicant(HrApplicant applicant)
        {
            OracleConnection conn = DbConnectionHelper.GetOrclConnection();
            StringBuilder insertQuery = new StringBuilder();
            insertQuery.Append("Insert into HR_APPLICANT (APPLICANT_ID, APPLICANT_NAME, GENDER, AGE, TOTAL_EXPERIENCES)");
            insertQuery.Append(" Values("+applicant.ApplicantId+", '"+applicant.Name+"', '"+applicant.Gender+"', "+applicant.Age+", "+applicant.TotalExperiences+")");
            OracleCommand cmd = new OracleCommand()
            {
                CommandText = insertQuery.ToString(),
                CommandType = CommandType.Text,
                Connection = conn
            };

            cmd.Connection.Open();
            var result = cmd.ExecuteNonQuery();

            StringBuilder expQuery = new StringBuilder();
            expQuery.Append("Insert into HR_EXPERIENCE (EXPERIENCE_ID, APPLICANT_ID, COMPANY_NAME, DESIGNATION, YEARS_WORKED)");

            //foreach (var exp in applicant.HrExperiences)
            //{
            //    expQuery.Append(" Select S_HR_EXPERIENCE.NEXTVAL,"+applicant.ApplicantId+", '"+exp.CompanyName+"', '"+exp.Designation+"', "+exp.YearsWorked+" from dual union all");
            //}

            var expRecordCount = applicant.HrExperiences.Count;

            for (int i = 0; i < applicant.HrExperiences.Count; i++)
            {
                expQuery.Append(" Select "+applicant.HrExperiences[i].ExperienceId+"," + applicant.ApplicantId + ", '" + applicant.HrExperiences[i].CompanyName + "', '" + applicant.HrExperiences[i].Designation + "', " + applicant.HrExperiences[i].YearsWorked + " from dual");
                if (i+1 < expRecordCount)
                {
                    expQuery.Append(" union all ");
                }
            }



            OracleCommand cmdExp = new OracleCommand()
            {
                CommandText = expQuery.ToString(),
                CommandType = CommandType.Text,
                Connection = conn
            };

            if (cmdExp.Connection.State == ConnectionState.Closed)
            {
                cmdExp.Connection.Open();
            }

            cmdExp.ExecuteNonQuery();

            cmd.Connection.Close();
            cmd.Dispose();
            cmdExp.Connection.Close();
            cmdExp.Dispose();

            return result;
        }

    }
}
