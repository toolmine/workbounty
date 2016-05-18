using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoWorkBounty.Models;
using DemoWorkBounty.Repository;
using DemoWorkBounty;

namespace DemoWorkBounty.Controllers
{
    public class WorkitemController : Controller
    {
        WorkitemRepository workbountyRepo = new WorkitemRepository();
        TeamRepository teamRepo = new TeamRepository();
        WorkBountyDBEntities entity = new WorkBountyDBEntities();

        public ActionResult ViewAssignedWorkitem(int currentWorkitemID)
        {
            try
            {
                var getDataofItemsIWantDone = workbountyRepo.GetAllitemsDone(currentWorkitemID);
                ViewBag.items = getDataofItemsIWantDone;

                var getDataofAppliedWorkitem = workbountyRepo.AppliedWorkitems(currentWorkitemID);
                ViewBag.apply = getDataofAppliedWorkitem;

                return View();
            
            }
          catch (Exception)
            {
                return RedirectToAction("Dashboard", "Home");
            }
        }

        [HttpPost]
        public JsonResult ViewAssignedWorkitem(WorkitemDistribution getWorkitemData)
        {
            var showAssignedWorkitemDetail = workbountyRepo.WorkitemDistribution(getWorkitemData);
            return Json(showAssignedWorkitemDetail);

        }

        public ActionResult UpdateWorkitem(int currentWorkitemID)
        {
            try
            {
            int currentUserID = Convert.ToInt32(Session["UserID"]);
            Session["UploadWorkitemID"] = currentWorkitemID;
            var getDataofCurrentWorkitem = workbountyRepo.ShowCurrentWorkitems(currentWorkitemID);
            var getDataofUploadDocument = workbountyRepo.UserUploadDocument(currentWorkitemID, currentUserID);
            ViewBag.dateOfSubmittedWork = getDataofUploadDocument;

            var currentDate = DateTime.Now;
            var workitemInfo = entity.Workitems.Where(s => s.WorkitemID == currentWorkitemID).FirstOrDefault();
            if (getDataofCurrentWorkitem.Count != 0)
            {
                if (currentDate < workitemInfo.DueDate)
                {
                    if (getDataofCurrentWorkitem != null)
                    {
                        ViewBag.dataForWorkitem = getDataofCurrentWorkitem;
                    }
                    else
                    {
                        ViewBag.blankDataMessage = "Blank data Error";
                    }

                }
                else
                {
                    ViewBag.displayMessage = "Due date is reached. Cannot upload document";
                }
            }
            else
            {
                ViewBag.displayAlert = "This workitem is already rewarded";
            }
            return View();
            }
            catch (Exception)
            {
                return RedirectToAction("Dashboard", "Home");
            }
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
                    int currentUserID = Convert.ToInt32(Session["UserID"]);
                    int currentWorkitemID = Convert.ToInt32(Session["UploadWorkitemID"]);
                    var getDataofUploadDocument = workbountyRepo.UserUploadDocument(currentWorkitemID, currentUserID);
                    ViewBag.dateOfSubmittedWork = getDataofUploadDocument;

                    WorkItemAssignment assignData = new WorkItemAssignment();
                    int Workid = Convert.ToInt32(data["Workid"].ToString());
                    var fileName = Path.GetFileName(myFile.FileName);
                    var path = Path.Combine(Server.MapPath("~/work1/Download/"), fileName);
                    myFile.SaveAs(path);
                    assignData.WorkItemID = Workid;
                    assignData.UserID = Convert.ToInt32(Session["UserID"]);
                    assignData.IsRewarded = false;
                    assignData.SubmissionDateTime = DateTime.Now;
                    assignData.SubmissionPath = path;
                    var putAssignData = workbountyRepo.UpdateWorkitems(assignData);
                    return RedirectToAction("dashboard", "home");
                }
            }
            catch (Exception)
            {
                ViewBag.blankDataMessage = "Blank data";
                return View();
            }
        }

        public ActionResult ApplyWorkitem(int currentWorkitemID)
        {
            try
            {
                var applyForWorkitemResult = workbountyRepo.GetWorkDetails(currentWorkitemID);
                return View(applyForWorkitemResult);
            }
            catch (Exception)
            {
                return RedirectToAction("Dashboard", "Home");
            }
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
            try
            {
                var getCheckDocument = workbountyRepo.CheckDocument(currentWorkitemID);
                if (getCheckDocument != null)
                {
                    var getDataofUploadDocument = workbountyRepo.ShowDocument(currentWorkitemID);
                    if (getDataofUploadDocument != null)
                    {
                        ViewBag.dataofOpenDocument = getDataofUploadDocument;
                    }
                    else
                    {
                        ViewBag.displayMessage = "Already send a reward";
                    }
                }
                else
                {
                    ViewBag.errorMessage = 0;
                }
                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("Dashboard", "Home");
            }
        }

        public ActionResult updateTask()
        {
            return View();
        }

        public ActionResult detailWorkitem(int currentWorkitemID)
        {
            try
            {
                var getDetailWorkitemData = workbountyRepo.GetWorkDetails(currentWorkitemID);
                return View(getDetailWorkitemData);
            }
            catch (Exception)
            {
                return RedirectToAction("Dashboard", "Home");
            }
        }

        public FileResult Download(int currentUserID, int workitemID)
        {
          
                var files = entity.WorkItemAssignments.Where(s => s.UserID == currentUserID && s.WorkItemAssignmentID == workitemID).Select(s => s.SubmissionPath).FirstOrDefault();
                string fileName = entity.WorkItemAssignments.Where(s => s.UserID == currentUserID && s.WorkItemAssignmentID == workitemID).Select(s => s.SubmissionPath).FirstOrDefault();
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
