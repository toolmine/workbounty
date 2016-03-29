using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DemoWorkBounty.Repository
{
    public class DetailWorkitemRepo : ApiController
    {
        private WorkBountyDBEntities entity = new WorkBountyDBEntities();

        public IEnumerable<Workitem> GetDetailItem(Workitem id)
        {


            var currentUserID = id;
            var assignUserId = entity.WorkItemAssignments.Where(a => a.UserID.Equals(id.WorkitemID) && a.IsRewarded == true);

            if (assignUserId != null)
            {
                return entity.Workitems.AsEnumerable();
            }

            return new List<Workitem>();
        }

    }
}