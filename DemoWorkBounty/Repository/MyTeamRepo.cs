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
        private WorkBountyDBEntities5 entity = new WorkBountyDBEntities5();

        public List<TeamInfo> getAllItem(int id)
        {
            List<TeamInfo> team = new List<TeamInfo>();
            try
            {
                int currentUserid = id;
                var selectTeam = entity.Teams.Where(s => s.UserID == currentUserid).Select(s => s.TeamUserInfoID);
                foreach (var item in selectTeam)
                {
                    var data = entity.Teams.Where(s => s.TeamUserInfoID == item).ToList();
                    TeamInfo teamInfo = new TeamInfo();
                    teamInfo.TeamUserInfoID = item;
                    foreach (var _user in data)
                    {
                        TeamUserInfo _team = new TeamUserInfo { FirstName = _user.UserInfo.FirstName, Email = _user.UserInfo.Email, PhoneNumber = _user.UserInfo.PhoneNumber };
                        teamInfo.TeamUserList.Add(_team);
                    }
                    team.Add(teamInfo);
                }
            }
            catch (Exception)
            {
                return null;
            }
            return team;
        }
    }
}