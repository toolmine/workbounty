using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoWorkBounty.Repository;
using DemoWorkBounty.Models;
using System.Net;
using System.Web.Http;
using DemoWorkBounty;

namespace DemoWorkBounty.Controllers
{
    public class RewardController : Controller
    {
        RewardRepository rewardRepo = new RewardRepository();
        TeamRepository teamRepo = new TeamRepository();
        WorkBountyDBEntities6 entity = new WorkBountyDBEntities6();

        public ActionResult ShowRewards()
        {
            int currentUserID = Convert.ToInt32(Session["UserID"]);
            var getListOfUserReward = rewardRepo.GetAllRewards(currentUserID);
            return View(getListOfUserReward);
        }

     
     }
}
