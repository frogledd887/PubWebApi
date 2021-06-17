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
    /// 
    /// </summary>
    public class DepaController : ApiController
    {
        private readonly SqlConnectionFactory sqlserver_conn;
        private DepaController()
        {
            //oracle_conn = new OracleConnectionFactory();
            sqlserver_conn = new SqlConnectionFactory();
        }
        /// <summary>
        /// 取得本局全部單位代碼及單位名稱 (不包含停用單位)
        /// </summary>
        /// <returns>
        /// 本局全部單位代碼及單位名稱
        /// </returns>
        [HttpGet]
        [Route("Depa/GetDepa")]
        public IHttpActionResult GetDepa()
        {
            using (var cn = sqlserver_conn.CreateConnection("PDDBSV05"))
            {
                string sql = $@"SELECT DEPA_CODE, REPLACE(REPLACE(DEPA_NAME, ' ', ''), '　', '')  AS DEPA_NAME
                                        FROM PP.T_EMPL_DEPA 
                                        WHERE   (NOT (DEPA_CODE IS NULL)) AND (DEPA_STOP IS NULL)
                                        ORDER BY DEPA_ND_CODE";
                var result = cn.Query(sql);
                return Json(JArray.FromObject(result));
            }
        }

        /// <summary>
        /// 依據「單位代碼」回傳「單位名稱」 (包含已停用單位)
        /// </summary>
        /// <param name="id">單位代碼</param>
        /// <returns>
        /// 單位資料內容
        ///  </returns>
        [HttpGet]
        [Route("Depa/GetDepaById/{id}")]
        public IHttpActionResult GetDepaById(string id)
        {
            using (var cn = sqlserver_conn.CreateConnection("PDDBSV05"))
            {
                string sql = $@"SELECT DEPA_CODE, REPLACE(REPLACE(DEPA_NAME, ' ', ''), '　', '')  AS DEPA_NAME
                                        FROM PP.T_EMPL_DEPA 
                                        WHERE   (NOT (DEPA_CODE IS NULL)) AND (DEPA_STOP IS NULL) AND (SUBSTRING(DEPA_CODE, 1, {id.ToString().Length}) = '{id}')
                                        ORDER BY DEPA_ND_CODE";
                var result = cn.Query(sql);
                return Json(JArray.FromObject(result));
            }
        }

        /// <summary>
        /// 依據「單位代碼」回傳「單位名稱」 (包含已停用單位)
        /// </summary>
        /// <param name="id">單位代碼</param>
        /// <returns>
        /// 單位資料內容
        ///  </returns>
        [HttpGet]
        [Route("Depa/GetDepaNameById/{id}")]
        public IHttpActionResult GetDepaNameById(string id)
        {
            using (var cn = sqlserver_conn.CreateConnection("PDDBSV05"))
            {
                string sql = $@"SELECT DEPA_CODE, REPLACE(REPLACE(DEPA_NAME, ' ', ''), '　', '') + IIF(DEPA_STOP='*','( 停用 )','') AS DEPA_NAME ,DEPA_STOP
                                        FROM PP.T_EMPL_DEPA
                                        WHERE DEPA_CODE = '{id}' ";
                var result = cn.Query(sql);
                return Json(JArray.FromObject(result));
            }
        }
    }
}
