using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DemoWorkBounty.Models;
using DemoWorkBounty;

namespace DemoWorkBounty.Repository
{
    public class RewardRepository : ApiController
    {
        private WorkBountyDBEntities entity = new WorkBountyDBEntities();
        public List<Rewards> GetAllRewards(int currentUserID)
        {
        
            var assignUserId = entity.WorkItemAssignments.Where(a => a.UserID.Equals(currentUserID) && a.IsRewarded == true).ToList();
            List<Rewards> displayRewarddata = new List<Rewards>();

            if (assignUserId != null)
            {
                foreach (var data in assignUserId)
                {
                    displayRewarddata = entity.WorkItemAssignments.Where(a => a.UserID.Equals(currentUserID) && a.IsRewarded == true).Select(s => new Rewards { Title = s.Workitem.Title, FirstName = s.Workitem.UserInfo.FirstName, ProposedReward = s.Workitem.ProposedReward, Amount = s.Workitem.Amount }).ToList();
                }
                return displayRewarddata;
             
            }
            else
            {
                return null;
            }
        }

       
    }
}