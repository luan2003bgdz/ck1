using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class DanhMucController : ApiController

    {
        CSDLTestEntities db = new CSDLTestEntities();
        [HttpGet]
        public List<DANHMUC> getAll()
        {
            return db.DANHMUCs.ToList();
        }
    }
}
