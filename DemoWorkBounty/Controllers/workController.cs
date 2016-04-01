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
        WorkBountyDBEntities5 entity = new WorkBountyDBEntities5();
        DetailItemRepo repo = new DetailItemRepo();
        public ActionResult updateTask()
        {
            return View();
        }

        public ActionResult detailWorkitem(int id)
        {
           
         
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

        public ActionResult AddDocument(int id)
          {
              var data = repo.ShowMyWorkitems(id);
              return View(data);
          }
       

    }
}
