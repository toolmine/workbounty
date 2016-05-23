using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DemoWorkBounty.Models;
using DemoWorkBounty;

namespace DemoWorkBounty.Repository
{
    public class TeamRepository : ApiController
    {
        WorkBountyDBEntities entity = new WorkBountyDBEntities();
        public List<TeamInformation> GetTeamList(int id)
        {
            List<TeamInformation> team = new List<TeamInformation>();
            try
            {
                int currentUserid = id;
                var selectTeam = entity.Teams.Where(s => s.UserID == currentUserid).Select(s => s.TeamUserInfoID);
                foreach (var item in selectTeam)
                {
                    var data = entity.Teams.Where(s => s.TeamUserInfoID == item).ToList();
                    TeamInformation teamInfo = new TeamInformation();
                    teamInfo.TeamUserInfoID = item;
                    foreach (var _user in data)
                    {
                        TeamUserInfo _team = new TeamUserInfo { FirstName = _user.UserInfo.FirstName, Email = _user.UserInfo.Email, PhoneNumber = _user.UserInfo.PhoneNumber, TeamName = _user.TeamName };
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
        public List<UserInfo> GetMemberResult(string memberName)
        {
            try
            {
                var getMemberData = entity.UserInfoes.Where(s => s.LastName.ToLower().StartsWith(memberName)
                              || s.FirstName.ToLower().StartsWith(memberName)).ToList();
                if (getMemberData == null)
                {
                    return null;
                }
                return getMemberData;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int AddTeamData(Team teamData)
        {                      
            try
            {
                int i = 0;
                
                do
                {
                    var checkTeamName = entity.Teams.Where(s => s.TeamName == teamData.TeamName).FirstOrDefault();
                    if (checkTeamName == null)
                    {
                          var qwe = entity.Teams.Where(s => s.TeamUserInfoID == teamData.TeamUserInfoID).FirstOrDefault();
                           if (qwe == null)
                                {
                                    var getID = Convert.ToInt32(teamData.TeamUserInfoID);
                                    entity.Teams.Add(teamData);
                                    entity.SaveChanges();
                                    i++;
                                    return getID;
                                }
                          else
                                {
                                    teamData.TeamUserInfoID = teamData.TeamUserInfoID + 1;
                                }
                    }
                    else
                    {
                        return 0;
                    }
                
                }
                while (i == 0);
                return 0;

            }
            catch (Exception)
            {
                return 0;
            }
        }

        public string AddUpdateMemberData(Team updateMemberData)
        {
            try
            {
                var checkTeamName = entity.Teams.Where(s => s.TeamUserInfoID == updateMemberData.TeamUserInfoID).Select(s => s.TeamName).FirstOrDefault();
                updateMemberData.TeamName = checkTeamName;
                var checkExistingUser = entity.Teams.Where(s => s.TeamUserInfoID == updateMemberData.TeamUserInfoID && s.UserID == updateMemberData.UserID).Any(s => s.UserID == updateMemberData.UserID);
                if (checkExistingUser == false)
                {
                    entity.Teams.Add(updateMemberData);
                    entity.SaveChanges();
                    return "Success";

                }
                return "Error";
            }
            catch (Exception)
            {
                return "Error";

            }
        }



        public string AddMemberData(Team memberData)
        {
            try
            {
                var checkExistingUser = entity.Teams.Where(s=>s.TeamUserInfoID==memberData.TeamUserInfoID && s.UserID==memberData.UserID).Any(s => s.UserID == memberData.UserID);
                    if (checkExistingUser == false)
                    {
                        entity.Teams.Add(memberData);
                        entity.SaveChanges();
                        return "Success";
                    
                    }
             
                
                return "Error";
            }
            catch (Exception)
            {
                return "Error";

            }
        }

        public string UpdateMemberData(Team memberData)
        {
            try
            {
                Team teamData = new Team();
                teamData = entity.Teams.Where(s => s.TeamUserInfoID == memberData.TeamUserInfoID && s.UserID == memberData.UserID).FirstOrDefault();
                if(teamData!=null)
                {
                entity.Teams.Remove(teamData);
                entity.SaveChanges();
                return "Success";
                }
                else
                {
                    return "error";
                }
            }
            catch (Exception)
            {
                return "Error";

            }
        }



        public List<TeamInformation> GetTeamDetail(string TeamName)
        {
            
                List<TeamInformation> team = new List<TeamInformation>();
                var GetDetails = entity.Teams.Where(s => s.TeamName == TeamName).ToList();
                TeamInformation teamInfo = new TeamInformation();
                foreach (var data in GetDetails)
                {
                    TeamUserInfo _team = new TeamUserInfo { TeamName = data.TeamName, FirstName = data.UserInfo.FirstName, Email = data.UserInfo.Email, PhoneNumber = data.UserInfo.PhoneNumber, UserID = data.UserID, TeamUserInfoID = data.TeamUserInfoID };
                    teamInfo.TeamUserList.Add(_team);
                }
                team.Add(teamInfo);
                return team;
            }

    }
}