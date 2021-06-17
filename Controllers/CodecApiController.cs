using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAPI.Models.Security;

namespace WebAPI.Controllers
{
    public class CodecApiController : JsonNetController
    {
        [HttpPost]
        public ActionResult EncryptString(string encKey, string rawText)
        {
            SecurityManager.Authorize(Request);
            return Json(new ApiResult<byte[]>(
                CodecModule.EncrytString(encKey, rawText)));
        }

        public class DecryptParameter
        {
            public string EncKey { get; set; }
            public List<byte[]> EncData { get; set; }
        }

        [HttpPost]
        public ActionResult BatchDecryptData(DecryptParameter decData)
        {
            SecurityManager.Authorize(Request);
            return Json(new ApiResult<List<string>>(
                CodecModule.DecryptData(decData.EncKey, decData.EncData)));
        }
    }
}