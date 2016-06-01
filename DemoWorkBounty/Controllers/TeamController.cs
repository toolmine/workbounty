using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoWorkBounty.Repository;
using DemoWorkBounty.Models;
using DemoWorkBounty;


namespace DemoWorkBounty.Controllers
{
    public class TeamController : Controller
    {
        TeamRepository teamRepo = new TeamRepository();
        WorkitemRepository workbountyRepo = new WorkitemRepository();
        WorkBountyDBEntities entity = new WorkBountyDBEntities();

        public ActionResult AddTeam()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AddTeam(Team teamData)
        {
            if (ModelState.IsValid)
            {
                var getTeamList = teamRepo.AddTeamData(teamData);
                Session["TeamID"] = getTeamList;
                return Json(getTeamList);
            }
            else
            {
                return null;
            }
        }



        public ActionResult AddMember(string TeamName)
        {
            int currentUserID = Convert.ToInt32(Session["UserID"]);
            var teams = entity.Teams.Where(s => s.TeamName == TeamName).ToList();
           
            int i = 0;
            foreach (var team in teams)
            {
                if (team.UserID == currentUserID)
                { i++;
                ViewBag.TeamUserInfoId = team.TeamUserInfoID;
                }
            }
            if (teams.Any(a => a.UserID == currentUserID))
            {
                try
                {
                    ViewBag.TeamName = TeamName;
                    var getTeamDetail = teamRepo.GetTeamDetail(TeamName);
                    return View(getTeamDetail);
                }
                catch (Exception)
                {
                    return RedirectToAction("Dashboard", "Home");
                }
            }
            else
            {
                Response.Redirect("/Home/Authorize");
                return null;
            }
        }


        [HttpPost]
        public JsonResult AddMember(Team memberData)
        {
            memberData.TeamUserInfoID = Convert.ToInt32(Session["TeamID"]);
            var getMemberData = teamRepo.AddMemberData(memberData);
            return Json(getMemberData);

        }

        [HttpPost]
        public JsonResult UpdateMember(Team updateMemberData)
        {
            var success = false;
            var message = "";
            try
            {
                var getMemberDetails = teamRepo.UpdateMemberData(updateMemberData);
                if (getMemberDetails != null)
                {
                    success = true;
                    message = "member delete  successfully!";
                }
                else
                {
                    message = "Failed to remove member";
                }
            }
            catch (Exception)
            {
                message = "Error";
                return Json("Error");
            }
            return Json(new { success = success, message = message });
        }

        public JsonResult FindTeamMember(string id)
        {
            var getSearchMemberData = teamRepo.GetMemberResult(id);
            return Json(getSearchMemberData);


        }

        [HttpPost]
        public JsonResult UpdateNewMember(Team updateMemberData)
        {
            var getMemberResults = teamRepo.AddUpdateMemberData(updateMemberData);
            return Json(getMemberResults);
        }

        public ActionResult GetTeamMemberList(string teamName)
        {
            ViewBag.TeamName = teamName;
            var getTeamDetail = teamRepo.GetTeamDetail(teamName);
            return PartialView("_TeamMemberList", getTeamDetail);
        }

        [HttpPost]
        public JsonResult UpdateTeamName(Team teamName)
        {
            var redirectURL = "";
            var IsSuccess = false;
            var successAddWorkitemMessage = "";
            try
            {
                if (ModelState.IsValid)
                {
                    var getUpdateTeamName = teamRepo.getUpdateTeamName(teamName);
                    IsSuccess = true;
                    successAddWorkitemMessage = "Team name changed successfully!";
                    redirectURL = Url.Action("viewteams", "Home");

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

        [HttpPost]
        public JsonResult RemoveTeam(int currentTeamID)
        {
           
                var redirectURL = "";
                var IsSuccess = false;
                var successTeamRemoveMessage = "";
                try
                {
                var getTeamDetail = teamRepo.RemoveTeam(currentTeamID);
                if (getTeamDetail != null)
                {
                    redirectURL = Url.Action("viewteams", "Home");
                    IsSuccess = true;
                    successTeamRemoveMessage = "Team name changed successfully!";
                }
                else
                {
                    successTeamRemoveMessage = "Error while entering in Data";
                }
               
            }
           catch(Exception)
            {

            }
                return Json(new { IsSuccess = IsSuccess, successAddWorkitemMessage = successTeamRemoveMessage, redirectURL = redirectURL });
        }
    }
}
