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
    /// 廠商基本資料
    /// </summary>
    public class CustController : ApiController
    {
        private readonly OracleConnectionFactory _conn;
        private CustController()
        {
            _conn = new OracleConnectionFactory();
        }
        /// <summary>
        /// 取得廠商 (EM.T_CUST_BASI) 全部資料
        /// </summary>
        /// <returns>廠商清單</returns>
        [HttpGet]
        [Route("Cust/GetCustById")]
        public IHttpActionResult Get(string id)
        {
            using (var cn = _conn.CreateConnection("PL"))
            {
                string sql = @"SELECT * FROM EM.T_CUST_BASI WHERE CUST_UNIF_NO = :CUST_UNIF_NO";
                var result = cn.Query(sql, new { CUST_UNIF_NO = id });
                return Json(JArray.FromObject(result));
            }
        }

    }
}
