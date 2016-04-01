using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DemoWorkBounty.Repository
{
    public class AddTeamMemberRepo : ApiController
    {
        private WorkBountyDBEntities5 entity = new WorkBountyDBEntities5();
        
        public List<UserInfo> GetItemById(string id)
        {
            try
            { 
                 var item = entity.UserInfoes.Where(s => s.LastName.ToLower().StartsWith(id)
                               || s.FirstName.ToLower().StartsWith(id)).ToList();

                  if (item == null)
                      {

                           throw new HttpResponseException(HttpStatusCode.NotFound);
                       }

              return item;
            }
            catch(Exception)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }
     


        public string AddTeamData(Team teamData)
        {
            try
            {

                entity.Teams.Add(teamData);
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