using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace TerrainHRM.Helper
{
    public static class DbConnectionHelper
    {
        public static OracleConnection GetOrclConnection()
        {
            string dbString = //"Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.1.222)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = DMS) ));User ID=DMS;Password=dms";
            //"Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.1.222)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = DMS) ));User ID=dms_test;Password=dms123";
            "Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = Baitul)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = XE) ));User ID=DU;Password=du";
            OracleConnection con = new OracleConnection(dbString);
            return con;
        }

        public static int GetMaxPKValue(string tableName, string primaryColumn)
        {
            OracleConnection conn = GetOrclConnection();
            int result = 0;
            string query = "select nvl(max(" + primaryColumn + "),0) pkvalue from " + tableName;
            OracleCommand cmd = new OracleCommand()
            {
                CommandText = query,
                CommandType = System.Data.CommandType.Text,
                Connection = conn
            };

            cmd.Connection.Open();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                result = Convert.ToInt32(reader[0]);
            }
            cmd.Connection.Close();

            return result;
        }


        public static int GetGeneratedPK(string pTableName, string pPkColumnName)
        {
            int pkId = 0;
            OracleConnection con = GetOrclConnection();
            //var context = new UGIDOCDbContext();
            OracleCommand cmd = new OracleCommand()
            {
                CommandText = "SELECT FG_GEN_PK_ID('" + pTableName + "','" + pPkColumnName + "') FROM DUAL",
                CommandType = CommandType.Text,
                Connection = con
            };

            con.Open();
            OracleDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                pkId = reader.GetInt32(0);

            con.Close();

            return pkId;
        }
    }
}
