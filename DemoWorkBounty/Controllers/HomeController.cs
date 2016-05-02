using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Workbounty.Repository;
using Workbounty.Models;
//using PagedList;
using System.Web.Security;
using System.Data;
using System.IO;
using DemoWorkBounty;

namespace Workbounty.Controllers
{
    public class HomeController : Controller
    {
        UserRepository userRepo = new UserRepository();
        WorkitemRepository workbountyRepo = new WorkitemRepository();
        TeamRepository teamRepo = new TeamRepository();
        RewardRepository rewardRepo = new RewardRepository();
        WorkBountyDBEntities6 entity = new WorkBountyDBEntities6();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            Session.Clear();
            Response.Cookies.Clear();
            Session.RemoveAll();
            Session.Abandon();
            Session["FirstName"] = null;
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        [HttpPost]
        public JsonResult Login(UserInfo userLoginData)
        {
            var success = false;
            var message = "";
            var redirectURL = "";
            try
            {
                  var loginData = userRepo.UserLogin(userLoginData);
                    if (loginData != null)
                    {
                        Session["UserID"] = loginData.UserID;
                        Session["FirstName"] = loginData.FirstName;
                        FormsAuthentication.SetAuthCookie(loginData.FirstName, false);
                        success = true;
                        message = "login successfully!";
                        redirectURL = Url.Action("Dashboard", "Home");
                    }
                   else
                {
                    message = "Error in Input";
                }
            }
            catch (Exception)
            {
                message = "Error";
                return Json("Error");
            }
            return Json(new { success = success, message = message, redirectURL = redirectURL });
        }

        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Signup(UserInfo userSignupData)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userSignupInfo = userRepo.AddUserDetails(userSignupData);
                    Session["UserID"] = userSignupInfo.UserID;
                    Session["FirstName"] = userSignupInfo.FirstName;
                    FormsAuthentication.SetAuthCookie(userSignupInfo.FirstName, false);
                    return Json("Success");
                }

                else { 
                    return Json("false"); 
                    }

            }
            catch (Exception)
            { return Json("false"); }
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [Authorize]
        public ActionResult Dashboard()
        {
            int currentUserID = Convert.ToInt32(Session["UserID"]);
            var getAllWorkitemData = workbountyRepo.GetAllWorkitems(currentUserID);
            ViewBag.itemsForAllWorkitem = getAllWorkitemData;

            var getItemsIWantDoneData = workbountyRepo.ItemsIWantDone(currentUserID);
            ViewBag.itemsForIWantDone = getItemsIWantDoneData;

            var getWorkitemAssigntoMeData = workbountyRepo.GetCurrentWorkitem(currentUserID);
            ViewBag.itemsForAssigntoMe = getWorkitemAssigntoMeData;
            return View();
        }

        public ActionResult ViewTeams()
        {
            int currentUserID = Convert.ToInt32(Session["UserID"]);
            var getTeamData = teamRepo.GetTeamList(currentUserID);
            return View(getTeamData);
        }

        public ActionResult ViewRewards()
        {
            int currentUserID = Convert.ToInt32(Session["UserID"]);
            var getRewardData = rewardRepo.GetAllRewards(currentUserID);
            return View(getRewardData);
        }

        public ActionResult AddWorkitem()
        {
            int currentUserID = Convert.ToInt32(Session["UserID"]);
            var getCurrentUserTeamInfo = workbountyRepo.SelectTeam(currentUserID);

            ViewBag.TeamList = new SelectList(getCurrentUserTeamInfo, "TeamUserInfoID", "TeamName");
            return View();
        }

        [HttpPost]
        public JsonResult AddWorkitem(Workitem addWorkitemData)
        {
            var redirectURL = "";
            var IsSuccess = false;
            var successAddWorkitemMessage = "";
            try
            {
                if (ModelState.IsValid)
                {
                    var fileName = Path.GetFileName(addWorkitemData.DocumentFilePath);
                    var path = Path.Combine(Server.MapPath("~/work/Download/"), fileName);
                    addWorkitemData.DocumentFilePath = path;
                    var getResultsOfWorkitemData = workbountyRepo.AddWorkitem(addWorkitemData);
                    IsSuccess = true;
                    successAddWorkitemMessage = "Workitem Added successfully!";
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

        public ActionResult OpenWorkitem(int currentUserID)
        {
            var getResultsofWorkitemList = workbountyRepo.GetAllWorkitems(currentUserID);
            return Json(getResultsofWorkitemList);
        }

        public ActionResult AssignedWorkitem(int currentWorkitemID)
        {
            var getAssignWorkitemData = workbountyRepo.GetAllitemsDone(currentWorkitemID);
            ViewBag.items = getAssignWorkitemData;

            var getListofUserApplied = workbountyRepo.AppliedWorkitems(currentWorkitemID);
            ViewBag.apply = getListofUserApplied;

            return View();
        }
        [HttpPost]
        public JsonResult AssignedWorkitem(WorkitemDistribution setAssignedWorkitem)
        {
            var getResponseOfAssignData = workbountyRepo.WorkitemDistribution(setAssignedWorkitem);
            return Json(getResponseOfAssignData);
        }

        public List<AddWorkitems> GetAssignedWorkitem()
        {
            return workbountyRepo.GetCurrentWorkitem();
        }

        public ActionResult SearchWorkitem(string searchWorkitemValue)
        {
            var getWorkitemData = workbountyRepo.SearchWorkitems(searchWorkitemValue);
            if (getWorkitemData != null)
            {
                ViewBag.dataForSearchItem = getWorkitemData;
            }
            else
            {
                ViewBag.displayMessage = "No Result found";
            }

            return View();
        }

        //public ActionResult Viewitemsiwantdone(int? page)
        //{
        //    int currentUserID = Convert.ToInt32(Session["UserID"]);
        //    var getItemsIWantDoneData = workbountyRepo.ItemsIWantDone(currentUserID);

        //    int pageSize = 10;
        //    int pageNumber = (page ?? 1);
        //    return View(getItemsIWantDoneData.ToPagedList(pageNumber, pageSize));
        //}

        //public ActionResult Viewitemsimworkingon(int? page)
        //{
        //    int currentUserID = Convert.ToInt32(Session["UserID"]);
        //    var getWorkitemAssigntoMeData = workbountyRepo.GetCurrentWorkitem(currentUserID);

        //    int pageSize = 10;
        //    int pageNumber = (page ?? 1);
        //    return View(getWorkitemAssigntoMeData.ToPagedList(pageNumber, pageSize));
        //}

        //public ActionResult Viewitemsinterestedin(int? page)
        //{
        //    int currentUserID = Convert.ToInt32(Session["UserID"]);
        //    var getAllWorkitemData = workbountyRepo.GetAllWorkitems(currentUserID);

        //    int pageSize = 10;
        //    int pageNumber = (page ?? 1);
        //    return View(getAllWorkitemData.ToPagedList(pageNumber, pageSize));
        //}

        public ActionResult ViewUserData()
        {
            int currentUserID = Convert.ToInt32(Session["UserID"]);
            var getUserProfileData = userRepo.ViewUserProfileDetails(currentUserID);
            ViewBag.getUserData = getUserProfileData;
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }

     

    }
}
