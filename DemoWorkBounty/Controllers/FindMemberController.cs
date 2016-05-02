using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Workbounty.Models;
using Workbounty.Repository;
using DemoWorkBounty;


namespace Workbounty.Controllers
{
    public class FindMemberController : ApiController
    {
        TeamRepository teamRepo = new TeamRepository();
        
        public List<TeamUserInfo> GetMemberResult(string id)
        {
            List<TeamUserInfo> list = new List<TeamUserInfo>();
            var responce = teamRepo.GetMemberResult(id.ToLower());
            foreach (var item in responce)
            {
                var user = new TeamUserInfo() { UserID = item.UserID, FirstName = item.FirstName, Email = item.Email };
                list.Add(user);
            }
            return list;
        }
    }
}