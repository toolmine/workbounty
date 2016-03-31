using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoWorkBounty.Models
{
    public class TeamInfo
    {
        public TeamInfo()
        {
            TeamUserList = new List<TeamUserInfo>();
        }
        public Nullable<int> TeamUserInfoID { get; set; }
        public List<TeamUserInfo> TeamUserList { get; set; }
    }
    public class TeamUserInfo
    {
        public string TeamName { get; set; }
        public int UserID { get; set; }
        public bool IsActive { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
    }
}