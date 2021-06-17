using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using System.Data;
using System.Data.SqlClient;
//using BorG.SPM.DataTier;
using Newtonsoft.Json;
using PubWebApi.Models;
using PubWebApi.Connection;
using Dapper;
using Newtonsoft.Json.Linq;

namespace PubWebApi.Controllers
{

    /// <summary>
    /// EIP應用系統清單
    /// </summary>

    public class EipApplController : ApiController
    {
        private readonly OracleConnectionFactory _conn;
        private EipApplController()
        {
            _conn = new OracleConnectionFactory();
        }

        /// <summary>
        /// 取得所有 EIP 應用系統清單
        /// </summary>
        /// <returns>EIP 應用系統清單</returns>
        [HttpGet]
        [Route("Eip/GetApplst")]
        public IHttpActionResult Get()
        {
            using (var cn = _conn.CreateConnection("PL"))
            {
                string sql = @"SELECT APPL_ID, APPL_NAME, DEVP_ID, DEVP_NAME
                                    FROM PL.T_EIP_APPL_LIST
                                    WHERE(FG_WEBF_LIST = 'Y') AND(NOT(OPEN_CODE IS NULL))
                                    ORDER BY APPL_ID";
                var result = cn.Query(sql);
                return Json(JArray.FromObject(result));
            }
        }

        /// <summary>
        /// 依據「系統代號」取得 EIP 應用系統清單
        /// </summary>
        /// <param name="id">系統代號</param>
        /// <returns>EIP 應用系統清單</returns>
        [HttpGet]
        [Route("Eip/GetApplstById")]
        public IHttpActionResult Get(string id)
        {
            using (var cn = _conn.CreateConnection("PL"))
            {
                string sql = @"SELECT APPL_ID, APPL_NAME, DEVP_ID, DEVP_NAM 
                                    FROM PL.T_EIP_APPL_LIST 
                                    WHERE FG_WEBF_LIST='Y' AND APPL_ID = :APPL_ID";
                var result = cn.Query(sql, new { APPL_ID = id });
                return Json(JArray.FromObject(result));
            }
        }
    }
}
