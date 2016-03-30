using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoWorkBounty.Models
{
    public class ViewMyTeam
    {
        public int TeamID { get; set; }
        public string TeamName { get; set; }
        public int UserID { get; set; }
        public bool IsActive { get; set; }
        public Nullable<int> TeamUserInfoID { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
    }
}