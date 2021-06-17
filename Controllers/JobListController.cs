using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Newtonsoft.Json;
using DortsWebApi.Models;
using DortsWebApi.Connection;
using Dapper;
using Newtonsoft.Json.Linq;

namespace DortsWebApi.Controllers
{
    /// <summary>
    /// 專案代碼清單
    /// </summary>
        public class JobListController : ApiController
    {
        private readonly OracleConnectionFactory _conn;
        private JobListController()
        {
            _conn = new OracleConnectionFactory();
        }

        /// <summary>
        /// 取得所有專案清單
        /// </summary>
        /// <returns>專案清單</returns>
        [HttpGet]
        [Route("JobList")]
        public IEnumerable<JOB_NO_LIST> Get()
        {
            var cn = _conn.CreateConnection("public");
            string sql = @"SELECT * FROM PC.JOB_NO_LIST A,PC.T_LINE_NAME B WHERE A.LINE_ID=B.LINE_CD";
            var joblist = cn.Query<JOB_NO_LIST>(sql).ToList();
            return joblist;
        }

        /// <summary>
        /// 依據「專案代碼(工作代號或工程標號)」取得專案項目內容
        /// </summary>
        /// <param name="id">專案代碼(工作代號或工程標號)</param>
        /// <returns>專案項目內容</returns>
        [HttpGet]
        [Route("JobList/ProjbyId")]
        public IEnumerable<JOB_NO_LIST> ProjbyId(string id)
        {
            var cn = _conn.CreateConnection("public");
            string sql = @"SELECT * FROM PC.JOB_NO_LIST A,PC.T_LINE_NAME B 
                                WHERE A.LINE_ID=B.LINE_CD AND A.PROJ_ID = :PROJ_ID
                                ORDER BY A.LINE_ID, A.PROJ_ID";
            var joblist = cn.Query<JOB_NO_LIST>(sql, new { PROJ_ID = id.ToUpper() }).ToList();
            return joblist;
        }

        /// <summary>
        /// 依據「線別代碼」取得專案清單
        /// </summary>
        /// <param name="id">線別代碼</param>
        /// <returns>專案清單</returns>
        [HttpGet]
        [Route("JobList/ProjbyLine")]
        public IEnumerable<JOB_NO_LIST> ProjbyLine(string id)
        {
            var cn = _conn.CreateConnection("public");
            string sql = @"SELECT * FROM PC.JOB_NO_LIST A,PC.T_LINE_NAME B 
                                WHERE A.LINE_ID=B.LINE_CD AND A.LINE_ID = :LINE_ID
                                ORDER BY A.PROJ_ID";
            var joblist = cn.Query<JOB_NO_LIST>(sql, new { LINE_ID = id.ToUpper() }).ToList();
            return joblist;
        }

        /// <summary>
        /// 依據「專案類別」取得專案清單
        /// </summary>
        /// <param name="id">專案類別代碼 (R/J/D/I/C/A)</param>
        /// <returns>專案清單</returns>
        [HttpGet]
        [Route("JobList/ProjbyKind")]
        public IEnumerable<JOB_NO_LIST> ProjbyKind(string id)
        {
            var cn = _conn.CreateConnection("public");
            string sql = @"SELECT * FROM PC.JOB_NO_LIST  
                                WHERE JOB_PROJ_KIND = :JOB_PROJ_KIND";
            var joblist = cn.Query<JOB_NO_LIST>(sql, new { JOB_PROJ_KIND = id.ToUpper() }).ToList();
            return joblist;
        }

    }
}
