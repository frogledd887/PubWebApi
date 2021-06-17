using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using PubWebApi.Models;
using PubWebApi.Connection;
using Dapper;
using Newtonsoft.Json.Linq;

namespace PubWebApi.Controllers
{

    /// <summary>
    /// API網址：https://localhost:44392/API/SPM/Pub/xxxx
    /// </summary>

    public class EmplController : ApiController
    {
        //private readonly OracleConnectionFactory oracle_conn;
        private readonly SqlConnectionFactory sqlserver_conn;
        private EmplController()
        {
            //oracle_conn = new OracleConnectionFactory();
            sqlserver_conn = new SqlConnectionFactory();
        }

        //private string EmplDbColumns = "EMPL_SERI_NMBR, EMPL_NAME, POST_TEXT, OFFI_TELE_NMBR_EXTE, E_MAIL_ADDR, SERV_DEPA_NAME, WORK_DEPA_CODE";
        private string EmplDbColumns = $@"SELECT DISTINCT
                                                    P.EMPL_SERI_NMBR, P.EMPL_NAME, P.POST_TEXT, ISNULL(P.POST_STAT_TEXT, ' ') AS POST_STAT_TEXT,
                                                    REPLACE(REPLACE(D.DEPA_NAME, ' ', ''), '　', '') AS WORK_DEPA_NAME, P.WORK_DEPA_CODE, 
                                                    P.OFFI_TELE_NMBR_EXTE, P.E_MAIL_ADDR
                                        FROM   PP.V_EMPL_OPEN AS P INNER JOIN
                                                    PP.T_EMPL_DEPA AS D ON P.WORK_DEPA_CODE = D.DEPA_CODE";

        /////Oracle資料庫
        /////

        ///// <summary>
        ///// 取得員工 (PP V_EMPL_OPEN) 全部資料
        ///// </summary>
        ///// <returns>員工清單</returns>
        //[HttpGet]
        //[Route("Empl/OraGetEmpl")]
        //public IHttpActionResult GetAll()
        //{
        //    using (var cn = oracle_conn.CreateConnection("PP"))
        //    {
        //        string sql = @"SELECT * FROM PP.V_EMPL_OPEN";
        //        var result = cn.Query(sql);
        //        return Json(JArray.FromObject(result));
        //    }
        //}

        ///// <summary>
        ///// 依據「部門代碼」取得員工資料
        ///// </summary>
        ///// <param name="depa">部門代碼</param>
        ///// <returns>特定部門之員工清單</returns>
        //[HttpGet]
        //[Route("Empl/OraGetEmplByDepa/{depa}")]
        //public IHttpActionResult GetDataDepa(string depa)
        //{
        //    using (var cn = oracle_conn.CreateConnection("PP"))
        //    {
        //        string sql = $@"SELECT * 
        //                        FROM PP.V_EMPL_OPEN
        //                        WHERE WORK_DEPA_CODE = '{depa}'";
        //        var result = cn.Query(sql);
        //        return Json(JArray.FromObject(result));
        //    }
        //}

        ///// <summary>
        ///// 依據「員工編號」取得員工資料
        ///// </summary>
        ///// <param name="id">員工編號</param>
        ///// <returns>員工資料內容</returns>
        //[HttpGet]
        //[Route("Empl/OraGetEmplById/{id}")]
        //public IHttpActionResult GetDataID(string id)
        //{
        //    using (var cn = oracle_conn.CreateConnection("PP"))
        //    {
        //        string sql = $@"SELECT * 
        //                        FROM PP.V_EMPL_RESI_OPEN
        //                        WHERE EMPL_SERI_NMBR = '{id}'";
        //        var result = cn.Query(sql);
        //        return Json(JArray.FromObject(result));
        //    }
        //}

        ///SQL Server資料庫
        /// <summary>
        /// 取得員工 (PP V_EMPL_OPEN) 全部資料
        /// </summary>
        /// <returns>員工清單</returns>
        [HttpGet]
        [Route("Empl/GetEmpl")]
        public IHttpActionResult SQLGetAll()
        {
            using (var cn = sqlserver_conn.CreateConnection("PDDBSV05"))
            {
                string sql = $@"{EmplDbColumns}";
                var result = cn.Query(sql);
                return Json(JArray.FromObject(result));
            }
        }

        /// <summary>
        /// 依據「部門代碼」取得員工資料
        /// </summary>
        /// <param name="depa">部門代碼</param>
        /// <returns>部門員工清單</returns>
        [HttpGet]
        [Route("Empl/GetEmplByDepa/{depa}")]
        public IHttpActionResult GetEmplByDepa(string depa)
        {
            using (var cn = sqlserver_conn.CreateConnection("PDDBSV05"))
            {
                //string sql = $@"{EmplDbColumns}
                //                    FROM PP.V_EMPL_OPEN
                //                    WHERE WORK_DEPA_CODE = '{depa}'";
                string sql = $@"{EmplDbColumns}
                                        WHERE (SUBSTRING(P.WORK_DEPA_CODE, 1, {depa.ToString().Length}) = '{depa}')
                                        ORDER BY P.WORK_DEPA_CODE";
                var result = cn.Query(sql);
                return Json(JArray.FromObject(result));
            }
        }

        /// <summary>
        /// 依據「員工編號」取得員工資料
        /// </summary>
        /// <param name="id">員工編號</param>
        /// <returns>員工資料內容</returns>
        [HttpGet]
        [Route("Empl/GetEmplById/{id}")]
        public IHttpActionResult SQLGetDataID(string id)
        {
            using (var cn = sqlserver_conn.CreateConnection("PDDBSV05"))
            {
                string sql = $@"{EmplDbColumns}
                                        WHERE P.EMPL_SERI_NMBR = '{id}'";
                var result = cn.Query(sql);
                return Json(JArray.FromObject(result));
            }
        }

        /// <summary>
        /// 職稱清單
        /// </summary>
        /// <returns>員工職稱清單</returns>   
        [HttpGet]
        [Route("Empl/GetEmplPost")]
        public IHttpActionResult GetEmplPost()
        {
            using (var cn = sqlserver_conn.CreateConnection("PDDBSV05"))
            {
                string sql = $@"SELECT  POST_STAN_CODE, POST_TEXT  
                                    FROM PP.T_EMPL_STAN_POST
                                    WHERE   (DORT_FLG = 'Y') AND (PA_FLAG = 'Y') 
                                    ORDER BY POST_STAN_CODE";
                var result = cn.Query(sql);
                return Json(JArray.FromObject(result));
            }
        }

        /// <summary>
        /// 依據「員工號」查詢該員工當天差勤系統中所代理之同仁
        /// </summary>
        /// <param name="id">員工號</param>
        /// <returns>USER_ID：請假同仁, DEPU_USER_ID：代理同仁</returns>
        [HttpGet]
        [Route("Empl/GetEmplDepu/{id}")]
        public IHttpActionResult Get(string id)
        {
            using (var cn = sqlserver_conn.CreateConnection("PDCGSV03"))
            {
                string sql = $@"SELECT  PECARD_1 AS USER_ID, PECARD_2 AS DEPU_USER_ID FROM dbo.V_deputy_2 " +
                                        "WHERE (PECARD_2 = @PECARD_2) AND (CONVERT(VARCHAR(16), GETDATE(), 120) BETWEEN begintime AND endtime)";
                var result = cn.Query(sql, new { PECARD_2 = id });
                return Json(JArray.FromObject(result));
            }
        }
    }
}
