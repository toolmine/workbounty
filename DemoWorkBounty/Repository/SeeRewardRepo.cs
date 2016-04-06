using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DemoWorkBounty.Models;

namespace DemoWorkBounty.Repository
{
    public class SeeRewardRepo : ApiController
    {
        private WorkBountyDBEntities5 entity = new WorkBountyDBEntities5();
        public List<Workitem> GetAllRewards(int id)
        {
            var assignUserId = entity.Workitems.Where(a => a.WorkitemID.Equals(id)).ToList();
            if (assignUserId == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return assignUserId;
        }

    }
}