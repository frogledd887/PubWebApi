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
using System.Web.Http.Cors;

namespace WebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserWorkController : ApiController
    {
        private SqlConnectionFactory _conn;

        public UserWorkController()
        {
            _conn = new SqlConnectionFactory();
        }

        public IEnumerable<USER_WORK> GetAllUsers()
        {
            var cn = _conn.CreateConnection("WWWDB2");
            string sql = "SELECT * FROM dbo.T_USER_WORK";
            var users = cn.Query<USER_WORK>(sql);
            return users;
        }

        public IEnumerable<USER_WORK> GetUser(string id)
        {
            var cn = _conn.CreateConnection("WWWDB2");
            string sql = @"SELECT * FROM dbo.T_USER_WORK WHERE USER_ID = @USER_ID";
            var users = cn.Query<USER_WORK>(sql, new {USER_ID = id}).ToList();
            return users;
        }
    }
}
