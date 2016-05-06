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
        WorkBountyDBEntities6 entity = new WorkBountyDBEntities6();

        public ActionResult AddTeam()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AddTeam(Team teamData)
        {
            if(ModelState.IsValid)
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
            var getTeamDetail = teamRepo.GetTeamDetail(TeamName);
            return View(getTeamDetail);
        }

        
        [HttpPost]
        public JsonResult AddMember(Team memberData)
        {
            memberData.TeamUserInfoID =Convert.ToInt32(Session["TeamID"]);
            var getMemberData = teamRepo.AddMemberData(memberData);
            return Json(getMemberData);

        }

        [HttpPost]
        public ActionResult UpdateMember(Team updateMemberData)
        {
            var getMemberDetails = teamRepo.UpdateMemberData(updateMemberData);
            return View(getMemberDetails);
        }

   
      public JsonResult FindTeamMember(string id)
        {
            var getSearchMemberData = teamRepo.GetMemberResult(id);
            return Json(getSearchMemberData, JsonRequestBehavior.AllowGet);
        }
      
        [HttpPost]
        public JsonResult UpdateNewMember(Team updateMemberData)
      {
          var getMemberResults = teamRepo.AddUpdateMemberData(updateMemberData);
          return Json(getMemberResults);
      }



    }
}
