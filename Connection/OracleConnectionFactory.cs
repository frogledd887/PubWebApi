using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace PubWebApi.Connection
{
    public class OracleConnectionFactory
    {
        public IDbConnection CreateConnection(string dbName)
        {
            string db_source;
            string db_userid;
            string db_password;

            switch (dbName.ToUpper())
            {
                case "PUBLIC":
                    {
                        db_source = "PDDBP701";
                        db_userid = "JEMNETUSR";
                        db_password = "EM123NET";
                        break;
                    }
                case "PL":
                    {
                        db_source = "PDDBP701";
                        db_userid = "JENUSER";
                        db_password = "D10818";
                        break;
                    }
                case "PP":
                    {
                        db_source = "PDDBP701";
                        db_userid = "jwfuser";
                        db_password = "aaaaaa";
                        break;
                    }
                default:
                    {
                        throw new Exception("dbName 不存在。");
                    }
            }
            string ConnectionString = $@"DATA SOURCE=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={db_source})(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME={db_source})));Persist Security Info=True;User ID={db_userid};Password={db_password}";
            return new OracleConnection(ConnectionString);
        }
    }
}
