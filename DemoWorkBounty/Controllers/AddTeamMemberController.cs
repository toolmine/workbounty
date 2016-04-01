using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using DemoWorkBounty.Repository;
namespace DemoWorkBounty.Controllers
{
    public class AddTeamMemberController : ApiController
    {
        private WorkBountyDBEntities4 entity = new WorkBountyDBEntities4();
        static AddTeamMemberRepo repo = new AddTeamMemberRepo();

     

        public List<UserInfo> GetItemById(string id)
        {
            var responce = repo.GetItemById(id);
            return responce;

        }

       
        public string AddTeamData(Team teamData)
        {
           var responce = repo.AddTeamData(teamData);
         return responce;
        }

    }
}
