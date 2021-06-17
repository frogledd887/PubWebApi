using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;
using WebAPI.Connection;
using Dapper;
using Oracle.ManagedDataAccess.Client;
using System.Web.Http.Cors;

namespace WebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ApplListController : ApiController
    {
        private OracleConnectionFactory _conn;

        public ApplListController()
        {
            _conn = new OracleConnectionFactory();
        }

        //public IEnumerable<APPL_LIST> GetAllAppl()
        //{
        //    var cn = _conn.CreateConnection("PL");
        //    string sql = @"SELECT  A.APPL_ID, A.APPL_NAME, A.DEVP_ID, A.DEVP_NAME, B.E_MAIL_ADDR, B.OFFI_TELE_NMBR_EXTE " +
        //                        "FROM  PL.V_EIP_APPL_LIST A, PP.V_EMPL_OPEN B " +
        //                        "WHERE (A.DEVP_ID = B.EMPL_SERI_NMBR) AND (INSTR(APPL_ID, '_') = 0) AND (INSTR(APPL_NAME, '測') = 0)";
        //    var users = cn.Query<APPL_LIST>(sql).ToList();
        //    return users;
        //}

        //public IEnumerable<APPL_LIST> GetAppl(string id)
        //{
        //    var cn = _conn.CreateConnection("PL");
        //    string sql = @"SELECT  A.APPL_ID, A.APPL_NAME, A.DEVP_ID, A.DEVP_NAME, B.E_MAIL_ADDR, B.OFFI_TELE_NMBR_EXTE " +
        //                        "FROM  PL.V_EIP_APPL_LIST A, PP.V_EMPL_OPEN B " +
        //                        "WHERE (A.DEVP_ID = B.EMPL_SERI_NMBR) AND (APPL_ID = :APPL_ID) ";
        //    var appl = cn.Query<APPL_LIST>(sql, new { APPL_ID = id }).ToList();
        //    return appl;
        //}

        public IEnumerable<APPL_LIST> GetAppl()
        {
            //Creating Web Service reference object  
            wsOracle.wsOracle objRef = new wsOracle.wsOracle();

            //calling and storing web service output into the variable  
            string sql = $@"SELECT  A.APPL_ID, A.APPL_NAME, A.DEVP_ID, A.DEVP_NAME, B.E_MAIL_ADDR, B.OFFI_TELE_NMBR_EXTE " +
                               "FROM  PL.V_EIP_APPL_LIST A, PP.V_EMPL_OPEN B " +
                               "WHERE (A.DEVP_ID = B.EMPL_SERI_NMBR) AND (INSTR(APPL_ID, '_') = 0) AND (INSTR(APPL_NAME, '測') = 0)";
            var ApplListData = objRef.GetDataSet("PL", sql);
            var appl = ApplListData.Tables[0].ConvertTo<APPL_LIST>().ToList();
            //returning josn result  
            return appl;
        }

        public IEnumerable<APPL_LIST> GetAppl(string id)
        {
            //Creating Web Service reference object  
            wsOracle.wsOracle objRef = new wsOracle.wsOracle();

            //calling and storing web service output into the variable  
            string sql = $@"SELECT  A.APPL_ID, A.APPL_NAME, A.DEVP_ID, A.DEVP_NAME, B.E_MAIL_ADDR, B.OFFI_TELE_NMBR_EXTE FROM  PL.V_EIP_APPL_LIST A, PP.V_EMPL_OPEN B  WHERE (A.DEVP_ID = B.EMPL_SERI_NMBR) AND (APPL_ID ='{id.ToUpper()}') ";
            var ApplListData = objRef.GetDataSet("PL",sql);
            var appl = ApplListData.Tables[0].ConvertTo<APPL_LIST>().ToList();
            //returning josn result  
            return appl;
        }
    }
}