using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DemoWorkBounty.Models;

namespace DemoWorkBounty.Repository
{
    public class ShowRewardRepo : ApiController
    {
        private WorkBountyDBEntities5 entity = new WorkBountyDBEntities5();
 
        public List<GetReward> GetAllRewards(int id)
        {
     
     
               var currentUserID=id;
               var assignUserId = entity.WorkItemAssignments.Where(a => a.UserID.Equals(id) && a.IsRewarded==true).ToList();
               List<GetReward> displaydata = new List<GetReward>();
              
                if(assignUserId == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
                else
                {
                    foreach(var data in assignUserId)
                    {
                        displaydata = entity.WorkItemAssignments.Select(s => new GetReward { Title = s.Workitem.Title, FirstName = s.UserInfo.FirstName, ProposedReward = s.Workitem.ProposedReward, Amount = s.Workitem.Amount }).ToList();
                       
                    }
                    return displaydata;
                }
                
       }


        public List<Workitem> GetItemById(string id)
        {

            try
            {

                var data = entity.Workitems.Where(s => s.Title.StartsWith(id)).ToList();
                return data;
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

      
    }
}