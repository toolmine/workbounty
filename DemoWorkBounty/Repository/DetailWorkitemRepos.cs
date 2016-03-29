using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DemoWorkBounty.Repository
{
    public class DetailWorkitemRepos : ApiController
    {
        private WorkBountyDBEntities entity = new WorkBountyDBEntities();
        private List<Workitem> workitems = new List<Workitem>();

        public List<Workitem> GetAllitems(string id)
        {
            var currentUserID = id;
            var assignUserId = entity.Workitems.Where(a => a.WorkitemID.Equals(id)).ToList();

            if (assignUserId != null)
            {
                return assignUserId;
            }
            else
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            }

    }
}