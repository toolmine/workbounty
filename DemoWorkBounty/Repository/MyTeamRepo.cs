using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DemoWorkBounty.Models;

namespace DemoWorkBounty.Repository
{
    public class MyTeamRepo : ApiController
    {
        private WorkBountyDBEntities2 entity = new WorkBountyDBEntities2();
        
        public List<ViewMyTeam> getAllItem(int id)
        {
            try
            {
                int currentUserid = id;

                var data = entity.Teams.Where(s=>s.TeamUserInfoID==currentUserid).Select(s => new ViewMyTeam { TeamID = s.TeamID, TeamName=s.TeamName,TeamUserInfoID=s.TeamUserInfoID,Email=s.UserInfo.Email,PhoneNumber=s.UserInfo.PhoneNumber,FirstName=s.UserInfo.FirstName}).ToList();
                return data;
            }
            catch (Exception)
            {
                return null;
            }

        }


    }
}