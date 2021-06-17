using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;
using WebAPI.Connection;
using Dapper;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Web.Http.Cors;

namespace WebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DortsEmplController : ApiController
    {
        private OracleConnectionFactory _conn;

        public DortsEmplController()
        {
            _conn = new OracleConnectionFactory();
        }

        //public IEnumerable<EMPL_OPEN> GetUser(string id)
        //{
        //    var cn = _conn.CreateConnection("public");
        //    string sql = @"SELECT * FROM PP.V_EMPL_OPEN WHERE EMPL_SERI_NMBR = :EMPL_SERI_NMBR";
        //    var users = cn.Query<EMPL_OPEN>(sql, new { EMPL_SERI_NMBR = id }).ToList();
        //    return users;
        //}

        public IEnumerable<EMPL_OPEN> GetUser(string id)
        {
            //Creating Web Service reference object  
            wsOracle.wsOracle objRef = new wsOracle.wsOracle();

            //calling and storing web service output into the variable  
            string sql = $@"SELECT * FROM PP.V_EMPL_OPEN WHERE EMPL_SERI_NMBR = '{id}' ";
            DataSet dsEmpl = objRef.GetDataSet("PP", sql);
            var users = dsEmpl.Tables[0].ConvertTo<EMPL_OPEN>().ToList();
            //returning josn result  
            return users;
        }
    }
}
