using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoWorkBounty.Repository;
using System.IO;

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
        [HttpPost]
        public JsonResult AddDocument(FormCollection data, HttpPostedFileBase myFile)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json("Error");
                }
                else
                {

                    WorkItemAssignment assignData = new WorkItemAssignment();
                    int Workid = Convert.ToInt32(data["Workid"].ToString());
                    
                    var fileName = Path.GetFileName(myFile.FileName);
                        var path = Path.Combine(Server.MapPath("~/FileUpload/"), fileName);
                        myFile.SaveAs(path);
                        assignData.WorkItemID = Workid;
                        assignData.UserID = Convert.ToInt32(Session["UserID"]);
                        assignData.IsRewarded = false;
                        assignData.SubmissionDateTime = DateTime.Now;
                        assignData.SubmissionPath = fileName;
                    var putAssignData = repo.UpdateWorkitems(assignData);
                    return Json("Success");
                }

            }
            catch (Exception)
            {
                return Json("Error");
            }
       
        }


        public ActionResult ViewDocument()
        {
            return View();
        }
    }
}
