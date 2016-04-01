using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DemoWorkBounty.Repository
{
    public class DetailItemRepo : ApiController
    {
        private WorkBountyDBEntities4 entity = new WorkBountyDBEntities4();
        public List<Workitem> GetAllitems(int id)
        {


            var assignUserId = entity.Workitems.Where(a => a.WorkitemID.Equals(id)).ToList();

            if (assignUserId == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return assignUserId;
        }

        [HttpPost]
        public string  Register(WorkitemRegistration item)
        {
            try
            {
                        entity.WorkitemRegistrations.Add(item);
                        entity.SaveChanges();
                        return "Success";
            }

            catch (Exception)
            {
                return "Error";
            }
        }
    }
}