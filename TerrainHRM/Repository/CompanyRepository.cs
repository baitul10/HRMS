using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using TerrainHRM.Helper;
using TerrainHRM.Interfaces;
using TerrainHRM.Models;
using System.Data.OracleClient;

namespace TerrainHRM.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        public int CreateCompany(HrCompany company)
        {
            OracleConnection conn = DbConnectionHelper.GetOrclConnection();

            StringBuilder queryString = new StringBuilder();
            queryString.Append("INSERT INTO COMPANY_INFO_MST (CIM_ID, CIM_NAME, CIM_DETAILS,   CIM_LOGO, CIM_MOTO, CIM_SHORT_NAME,");
            queryString.Append("CIM_MULTI_COMPANY_FLAG,");
            queryString.Append("CIM_FILE_NAME, CIM_UPDATE_DATE, CIM_FILE_MIMETYPE,");
            queryString.Append("CIM_FILE_CHARACTER) ");
            queryString.Append("VALUES (" + company.CimId + ", '" + company.CimName + "', '" + company.CimDetails + "',  utl_raw.cast_to_raw('" + company.CimLogoApps + "'), '" + company.CimMoto + "', ");
            queryString.Append("'" + company.CimShortName + "', " + company.MultiCompanyFlag + ", '" + company.FileName + "', sysdate ");
            queryString.Append(", '" + company.FileMymeType + "' , '" + company.FileCharacter + "')");

            OracleCommand cmd = new OracleCommand()
            {
                CommandText = queryString.ToString(),
                CommandType = CommandType.Text,
                Connection = conn
            };

            cmd.Connection.Open();
            var restult = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            cmd.Dispose();
            return restult;
        }

        public HrCompany GetCompany()
        {
            HrCompany company = new HrCompany();
            StringBuilder getQuery = new StringBuilder();
            getQuery.Append("SELECT  CIM_ID, CIM_NAME, CIM_DETAILS, CIM_LOGO, CIM_MOTO, CIM_SHORT_NAME, CIM_MULTI_COMPANY_FLAG, ");
            getQuery.Append("   CIM_FILE_NAME, CIM_UPDATE_DATE, CIM_FILE_MIMETYPE, CIM_FILE_CHARACTER FROM COMPANY_INFO_MST ");
            getQuery.Append("  WHERE CIM_ID = 1 ");

            OracleConnection conn = DbConnectionHelper.GetOrclConnection();
            OracleCommand cmd = new OracleCommand()
            {
                CommandText = getQuery.ToString(),
                CommandType = System.Data.CommandType.Text,
                Connection = conn
            };

            cmd.Connection.Open();
            OracleDataReader reader = cmd.ExecuteReader();

            if (reader.RowSize>0)
            {

                    if (reader.Read())
                    {
                        //var company = new HrCompany();
                        company.CimId = Convert.ToInt32(reader[0]);
                        company.CimName = reader[1].ToString();
                        company.CimDetails = reader[2].ToString();
                        //company.CimLogoApps = (byte[])reader[3];// reader[3].ToString();
                        company.CimMoto = reader[4].ToString();
                        company.CimShortName = reader[5].ToString();
                        company.MultiCompanyFlag = Convert.ToInt32(reader[6]);
                        company.FileName = reader[7].ToString();
                        company.FileUpdateDate = Convert.ToDateTime(reader[8]);
                        company.FileMymeType = reader[9].ToString();
                        company.FileCharacter = reader[8].ToString();


                        //var imgCmd = new OracleCommand("SELECT photo FROM photos WHERE photo_id = 1", _con);
                        ///imgCmd.InitialLONGFetchSize = -1; // Retrieve the entire image during select instead of possible two round trips to DB
                        //var reader = imgCmd.ExecuteReader();
                        // if (reader.Read())
                        // {
                        // Fetch the LONG RAW
                        OracleBinary imgBinary = reader.GetOracleBinary(3);
                        // Get the bytes from the binary obj
                        byte[] imgBytes = imgBinary.IsNull ? null : imgBinary.Value;

                        company.CimLogoApps = imgBytes;
                        //}


                        

                    }

                    reader.Close();

               
            }
           

            cmd.Connection.Close();
            cmd.Dispose();

            return company;
        }

        public List<Employee> GetEmployees()
        {
            var employees = new List<Employee>();
            OracleConnection conn = DbConnectionHelper.GetOrclConnection();
            OracleCommand cmd = new OracleCommand()
            {
                Connection = conn,
                CommandText = "PG_GET_TEMP_DATE",
                CommandType = CommandType.StoredProcedure
            };

            var prmEmpId = new OracleParameter("P_ID", OracleDbType.Int32, ParameterDirection.Input);
            prmEmpId.Value = 10;

            var prmName = new OracleParameter("P_NAME", OracleDbType.Varchar2, ParameterDirection.Input)
            {
                Size = 200
            };
            prmName.Value = "Baitul";

            var prmEmpData = new OracleParameter("P_DATA", OracleDbType.RefCursor, ParameterDirection.Output);

            cmd.Parameters.Add(prmEmpId);
            cmd.Parameters.Add(prmName);
            cmd.Parameters.Add(prmEmpData);

            cmd.Connection.Open();
            //OracleDataReader reader = cmd.ExecuteReader();
            cmd.ExecuteNonQuery();

            OracleDataReader reader = ((OracleRefCursor)prmEmpData.Value).GetDataReader();

            while (reader.Read())
            {
                var emp = new Employee();

                emp.Id = Convert.ToInt32(reader[0]);
                emp.Name = reader[1].ToString();

                employees.Add(emp);
            };


            reader.Close();
            reader.Dispose();
            prmEmpId.Dispose();
            prmName.Dispose();
            prmEmpData.Dispose();
            cmd.Connection.Close();
            cmd.Connection.Dispose();
            cmd.Dispose();

            return employees;
        }
        public void UploadBlobObject(string filename)
        {
            FileStream stream = new FileStream(filename, FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(stream);
            try
            {
                using (OracleConnection connection = new OracleConnection())
                {
                    if (connection.State != ConnectionState.Open) connection.Open();
                    //OracleLob oracleLob = new OracleLob(connection, OracleDbType.Blob);
                    int streamLength = (int)stream.Length;
                   // oracleLob.Write(reader.ReadBytes(streamLength), 0, streamLength);
                    OracleCommand command = new OracleCommand("INSERT INTO Contact (FirstName, LastName, Photo) " + "VALUES('Joydip','Kanjilal', :Picture)", connection);
                    OracleParameter oracleParameter = command.Parameters.Add("Picture", OracleDbType.Blob);
                    //oracleParameter.OracleValue = oracleLob;
                    command.ExecuteNonQuery();
                }
            }
            catch
            {
                throw;
            }
        }


        public int EditCompanyInfo(HrCompany company)
        {
            OracleConnection conn = DbConnectionHelper.GetOrclConnection();
            StringBuilder editQuery = new StringBuilder();
            editQuery.Append("UPDATE COMPANY_INFO_MST ");
            editQuery.Append(" SET CIM_NAME = '" + company.CimName + "',");
            editQuery.Append(" CIM_DETAILS = '" + company.CimDetails + "',");
            editQuery.Append(" CIM_MOTO = '" + company.CimMoto + "',");
            editQuery.Append(" CIM_SHORT_NAME = '" + company.CimShortName + "',");
            editQuery.Append(" CIM_MULTI_COMPANY_FLAG = " + company.MultiCompanyFlag + ",");
            editQuery.Append(" CIM_FILE_NAME = '" + company.FileName + "',");
            editQuery.Append(" CIM_UPDATE_DATE = SYSDATE, CIM_FILE_MIMETYPE = '" + company.FileMymeType+"'");
            editQuery.Append(" WHERE CIM_ID = " + company.CimId);

            OracleCommand cmd = new OracleCommand()
            {
                Connection = conn,
                CommandText = editQuery.ToString(),
                CommandType = CommandType.Text
            };

            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            cmd.Dispose();


            return 1;
        }




    }

}
