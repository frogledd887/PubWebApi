using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PubWebApi.Models
{
    public class EMPL_OPEN
    {
        public string EMPL_SERI_NMBR { get; set; }
        public string EMPL_NAME { get; set; }
        public string WORK_DEPA_CODE { get; set; }
        public string SERV_DEPA_NAME { get; set; }
        public string POST_TEXT { get; set; }
        public string POST_STAT_TEXT { get; set; }
        public string OFFI_TELE_NMBR_EXTE { get; set; }
        public string E_MAIL_ADDR { get; set; }
    }
}