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
        private WorkBountyDBEntities3 entity = new WorkBountyDBEntities3();
        public List<Workitem> GetAllitems(int id)
        {
  

            var assignUserId = entity.Workitems.Where(a => a.WorkitemID.Equals(id)).ToList();

            if (assignUserId == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return assignUserId;
        }



        public string AddMemberData(Team memberData)
        {
            try
            {

                entity.Teams.Add(memberData);
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