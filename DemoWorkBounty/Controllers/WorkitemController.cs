using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Workbounty.Models;
using Workbounty.Repository;
using DemoWorkBounty;

namespace Workbounty.Controllers
{
    public class WorkitemController : Controller
    {
        WorkitemRepository workbountyRepo = new WorkitemRepository();
        TeamRepository teamRepo = new TeamRepository();
        WorkBountyDBEntities6 entity = new WorkBountyDBEntities6();
       
        public ActionResult ViewAssignedWorkitem(int currentWorkitemID)
        {

            var getDataofItemsIWantDone = workbountyRepo.GetAllitemsDone(currentWorkitemID);
            ViewBag.items = getDataofItemsIWantDone;

            var getDataofAppliedWorkitem = workbountyRepo.AppliedWorkitems(currentWorkitemID);
            ViewBag.apply = getDataofAppliedWorkitem;

            return View();
        }

        [HttpPost]
        public JsonResult ViewAssignedWorkitem(WorkitemDistribution getWorkitemData)
        {
            var showAssignedWorkitemDetail = workbountyRepo.WorkitemDistribution(getWorkitemData);
            return Json(showAssignedWorkitemDetail);

        }


        public ActionResult UpdateWorkitem(int currentWorkitemID)
        {
            var getDataofCurrentWorkitem = workbountyRepo.ShowCurrentWorkitems(currentWorkitemID);
            var currentDate = DateTime.Now;
            var workitemInfo = entity.Workitems.Where(s => s.WorkitemID == currentWorkitemID).FirstOrDefault();
            if (currentDate < workitemInfo.DueDate)
            {
                if (getDataofCurrentWorkitem != null)
                {
                    ViewBag.dataForWorkitem = getDataofCurrentWorkitem;
                }
                else
                {
                    ViewBag.displayMessage = "Already Submitted a Document";
                }
            }
            else 
            {
                ViewBag.displayMessage = "Due date is reached. Cannot upload document";
            }
            return View();
        }

        [HttpPost]
        public ActionResult UpdateWorkitem(FormCollection data, HttpPostedFileBase myFile)
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
                    var path = Path.Combine(Server.MapPath("~/work/Download/"), fileName);
                    myFile.SaveAs(path);
                    assignData.WorkItemID = Workid;
                    assignData.UserID = Convert.ToInt32(Session["UserID"]);
                    assignData.IsRewarded = false;
                    assignData.SubmissionDateTime = DateTime.Now;
                    assignData.SubmissionPath = path;
                    var putAssignData = workbountyRepo.UpdateWorkitems(assignData);
                    return RedirectToAction("dashboard","home");
                }
            }
            catch (Exception)
            {
                return View();
            }
        }

        public ActionResult ApplyWorkitem(int currentWorkitemID)
        {
            var applyForWorkitemResult = workbountyRepo.GetWorkDetails(currentWorkitemID);
            return View(applyForWorkitemResult);
        }

        [HttpPost]
        public JsonResult ApplyWorkitem(WorkitemRegistration dataForWorkitemRegistration)
        {
          var applyDataForWorkitem = workbountyRepo.UserRegisterForWorkitem(dataForWorkitemRegistration);
          return Json(applyDataForWorkitem);
                 
        }

        [HttpPost]
        public JsonResult RemoveFavourite(WorkitemRegistration dataForWorkitemRegistration)
        {
            var applyDataForWorkitem = workbountyRepo.RemoveFavouriteWorkitem(dataForWorkitemRegistration);
            return Json(applyDataForWorkitem);

        }
        
        public ActionResult ViewUpdatedWorkitem(int currentWorkitemID)
        {
            var getDataofUploadDocument = workbountyRepo.ShowDocument(currentWorkitemID);
            if (getDataofUploadDocument!=null)
            {
                ViewBag.dataofOpenDocument = getDataofUploadDocument;
            }
            else
            {
                ViewBag.displayMessage = "Already send a reward";
            }
            return View();
        }

        public ActionResult updateTask()
        {
            return View();
        }

        public ActionResult detailWorkitem(int currentWorkitemID)
        {
            var getDetailWorkitemData = workbountyRepo.GetWorkDetails(currentWorkitemID);
            return View(getDetailWorkitemData);
        }

        public FileResult Download(int currentUserID)
        {
            var files = entity.WorkItemAssignments.Where(s => s.UserID == currentUserID).Select(s => s.SubmissionPath).FirstOrDefault();
            string fileName = entity.WorkItemAssignments.Where(s => s.UserID == currentUserID).Select(s => s.SubmissionPath).FirstOrDefault();
            return File(files, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        [HttpPost]
        public JsonResult PayReward(Rewards currentUserID)
        {
            var getRewardData = workbountyRepo.ApplyReward(currentUserID);
            if (getRewardData != null)
            {
                return Json("Success");
            }
            else
            {
                return Json("Error");
            }
        }

      
      
    }
}
