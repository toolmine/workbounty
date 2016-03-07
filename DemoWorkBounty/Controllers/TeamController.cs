using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoWorkBounty.Controllers
{
    public class TeamController : Controller
    {
        //
        // GET: /Team/

        public ActionResult addMember()
        {
            return View();
        }

        public ActionResult Search()
        {
            return View();
        }


        public ActionResult popup()
        {
            return View();
        }
    }
}
