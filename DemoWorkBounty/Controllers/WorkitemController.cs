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
            int currentUserID = Convert.ToInt32(Session["UserID"]);
            var valid = entity.Workitems.Where(x => x.CreatedBy == currentUserID).Select(x => x.WorkitemID).ToList();
            try
            {
                if (valid.Contains(currentWorkitemID))
                {
                    var getDataofItemsIWantDone = workbountyRepo.GetAllitemsDone(currentWorkitemID);
                    ViewBag.items = getDataofItemsIWantDone;

                    var getDataofAppliedWorkitem = workbountyRepo.AppliedWorkitems(currentWorkitemID);
                    ViewBag.apply = getDataofAppliedWorkitem;

                    return View();
                }
                else
                {
                    Response.Redirect("/Home/Authorize");
                    return null;
                }
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
            int currentUserID = Convert.ToInt32(Session["UserID"]);
            var valid = entity.WorkitemRegistrations.Where(x => x.UserID == currentUserID && x.IsExclusive == false).Select(x => x.WorkitemID).ToList();
            var exclusiveValid = entity.WorkitemDistributions.Where(x => x.UserID == currentUserID && x.WorkitemID == currentWorkitemID).Select(x => x.WorkitemID).ToList();
            try
            {
                if (valid.Contains(currentWorkitemID) || exclusiveValid.Contains(currentWorkitemID))
                {
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
                }
                else
                {
                    Response.Redirect("/Home/Authorize");
                    return null;
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
            int inc = 0;
            int currentUserID = Convert.ToInt32(Session["UserID"]);
            var valid = workbountyRepo.GetAllWorkitems(currentUserID);
            foreach (var item in valid)
            {
                if (item.WorkitemID == currentWorkitemID)
                {
                    inc++;
                }
            }
            try
            {
                if (inc > 0)
                {
                    var applyForWorkitemResult = workbountyRepo.GetWorkDetails(currentWorkitemID);
                    return View(applyForWorkitemResult);
                }
                else 
                {
                    Response.Redirect("/Home/Authorize");
                    return null;
                }
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
             int currentUserID = Convert.ToInt32(Session["UserID"]);
             var valid = entity.Workitems.Where(x => x.CreatedBy == currentUserID).Select(x => x.WorkitemID).ToList();
             try
             {
                 if (valid.Contains(currentWorkitemID))
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
                }
                else 
                {
                    Response.Redirect("/Home/Authorize");
                    return null;
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

        public ActionResult EditWorkitem(int currentWorkitemID)
        {
            int currentUserID = Convert.ToInt32(Session["UserID"]);
            var valid = entity.Workitems.Where(x => x.CreatedBy == currentUserID).Select(x => x.WorkitemID).ToList();
            try
            {
                if (valid.Contains(currentWorkitemID))
                {
                    var getCurrentUserTeamInfo = workbountyRepo.SelectTeam(currentUserID);

                    ViewBag.TeamList = new SelectList(getCurrentUserTeamInfo, "TeamUserInfoID", "TeamName");
                    var getDataofItemsIWantDone = workbountyRepo.GetAllitemsDone(currentWorkitemID);
                    ViewBag.items = getDataofItemsIWantDone;

                    return View();
                }
                else
                {
                    Response.Redirect("/Home/Authorize");
                    return null;
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Dashboard", "Home");
            }
        }

        [HttpPost]
        public JsonResult EditWorkitem(Workitem addWorkitemData)
        {
            var redirectURL = "";
            var IsSuccess = false;
            var successAddWorkitemMessage = "";
            try
            {
                if (ModelState.IsValid)
                {
                    var getResultsOfWorkitemData = workbountyRepo.EditWorkitem(addWorkitemData);
                    IsSuccess = true;
                    successAddWorkitemMessage = "Workitem Updated successfully!";
                    redirectURL = Url.Action("Dashboard", "Home");

                }
                else
                {
                    successAddWorkitemMessage = "Error while entering in Data";
                }
            }
            catch (Exception)
            {
                successAddWorkitemMessage = "Error";
                return Json("Error");
            }
            return Json(new { IsSuccess = IsSuccess, successAddWorkitemMessage = successAddWorkitemMessage, redirectURL = redirectURL });

        }


    }
}
