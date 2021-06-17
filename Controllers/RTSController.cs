using System.Data;
using System.Data.SqlClient;
using System.Web.Http;

namespace DortsWebApi.Controllers
{
    /// <summary>
    /// 博格公用API
    /// </summary>
    public class RTSController : ApiController
    {
        [HttpGet]
        [Route("BPMS/")]
        public DataTable ApplyInfo(string empl)
        {
            string connStr = @"server=.; database=HR; uid=sa; pwd=123";
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand selectComd = new SqlCommand("", conn);
                selectComd.CommandText = @"SELECT * FROM [HR].[dbo].[EMPL] WHERE [EMPL_SERI_NMBR] = @EMPL_SERI_NMBR";
                selectComd.Parameters.AddWithValue("@EMPL_SERI_NMBR", empl);
                SqlDataAdapter selectDA = new SqlDataAdapter(selectComd);
                DataTable selectDT = new DataTable();
                selectDA.Fill(selectDT);

                return selectDT;
            }
        }
    }
}
