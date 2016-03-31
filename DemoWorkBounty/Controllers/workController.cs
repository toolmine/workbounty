using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoWorkBounty.Repository;

namespace DemoWorkBounty.Controllers
{
    public class workController : Controller
    {
        public ActionResult updateTask()
        {
            return View();
        }

        public ActionResult detailWorkitem(int id)
        {
            WorkBountyDBEntities3 entity = new WorkBountyDBEntities3();
            DetailItemRepo repo = new DetailItemRepo();
            var item = repo.GetAllitems(id);

            return View(item);
        }




        public ActionResult addWorkitem()
        {
            return View();
        }

          public ActionResult Index()
        {
            return View();
         }

        public ActionResult tourist()
          {
              return View();
          }

        public ActionResult gPlugin()
        {
            return View();
        }

        public ActionResult AppletServ()
        {
            return View();
        }


        public ActionResult BusinessPortal()
        {
            return View();
        }

        public ActionResult Payroll()
        {
            return View();
        }

        public ActionResult webpage()
        {
            return View();
        }

        public ActionResult machine()
        {
            return View();
        }

        public ActionResult bet()
        {
            return View();
        }

        public ActionResult Transection()
        {
            return View();
        }

        public ActionResult javaApp()
        {
            return View();
        
        }

        public ActionResult embeddedsystem()
        {
            return View();

        }
        public ActionResult desktopaccount()
        {
            return View();

        }
        public ActionResult ios()
        {
            return View();

        }

        public ActionResult databasedevelopment()
        {
            return View();

        }

       

    }
}
