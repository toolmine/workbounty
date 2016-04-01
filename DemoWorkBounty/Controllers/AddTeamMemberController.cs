using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using DemoWorkBounty.Repository;
using DemoWorkBounty.Models;
namespace DemoWorkBounty.Controllers
{
    public class AddTeamMemberController : ApiController
    {
        private WorkBountyDBEntities5 entity = new WorkBountyDBEntities5();
        static AddTeamMemberRepo repo = new AddTeamMemberRepo();



        public List<TeamUserInfo> GetItemById(string id)
        {
            List<TeamUserInfo> list = new List<TeamUserInfo>();
            var responce = repo.GetItemById(id.ToLower());
            foreach (var item in responce)
            {
                var user = new TeamUserInfo() { UserID = item.UserID, FirstName = item.FirstName, Email = item.Email };
                list.Add(user);
            }
            return list;
            

        }

       
        public string AddTeamData(Team teamData)
        {
           var responce = repo.AddTeamData(teamData);
         return responce;
        }

    }
}
