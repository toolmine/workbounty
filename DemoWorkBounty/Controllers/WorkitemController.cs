﻿using System;
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
            int currentUserID = Convert.ToInt32(Session["UserID"]);
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
            var getCheckDocument = workbountyRepo.CheckDocument(currentWorkitemID);
            if(getCheckDocument!=null)
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

            }
           else
            {
                ViewBag.errorMessage = 0;
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

        public FileResult Download(int currentUserID,int workitemID)
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
