using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DemoWorkBounty.Repository
{
    public class ShowRewardRepo : ApiController
    {
        private WorkBountyDBEntities4 entity = new WorkBountyDBEntities4();
 
        public List<WorkItemAssignment> GetAllRewards(int id)
        {
     
     
            var currentUserID=id;
               var assignUserId = entity.WorkItemAssignments.Where(a => a.UserID.Equals(id) && a.IsRewarded==true).ToList();
                    
                if(assignUserId == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }

                return assignUserId;
       }

      
    }
}