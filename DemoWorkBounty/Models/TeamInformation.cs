using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DemoWorkBounty.Models
{
    public class TeamInformation
    {
        public TeamInformation()
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
        public int PublishedTo { get; set; }
        public Nullable<int> TeamUserInfoID { get; set; }
       
    }

    public class UserProfileInfo
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public System.DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string InterestedKeywords { get; set; }
        

    }
}
     