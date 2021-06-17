using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace PubWebApi.Connection
{
    public class SqlConnectionFactory
    {
        public IDbConnection CreateConnection(string name)
        {
            switch (name.ToUpper())
            {
                case "WWWDB2":
                    {
                        var ConnectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[name].ConnectionString;
                        return new SqlConnection(ConnectionString);
                    }
                case "PDCGSV03":
                    {
                        //差勤資料
                        string db_source = name;
                        string db_userid = "pmuser";
                        string db_password = "Pm#8021mp";
                        string database = "Dorts_TCGPSN";
                        string ConnectionString = $@"Server={db_source};
                                                     Database={database};
                                                     MultipleActiveResultSets=true;
                                                     User ID={db_userid};
                                                     Password={db_password}";
                        return new SqlConnection(ConnectionString);
                    }
                case "PDDBSV05":
                    {
                        //測試SQL Server PP人事資料庫
                        string db_source = "PDDBSV05";
                        string db_userid = "ppdorts";
                        string db_password = "AAAAAA";
                        string database = "PP";
                        string ConnectionString = $@"Server={db_source};
                                                     Database={database};
                                                     MultipleActiveResultSets=true;
                                                     User ID={db_userid};
                                                     Password={db_password}";
                        return new SqlConnection(ConnectionString);
                    }
                default:
                    {
                        throw new Exception("name 不存在。");
                    }
            }
        }
    }
}