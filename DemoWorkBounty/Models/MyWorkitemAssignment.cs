using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoWorkBounty.Models
{
    public class MyWorkitemAssignment
    {
        public int UserID { get; set; }
        public int WorkItemID { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public string FirstName { get; set; }
        public string CreatedBy { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public string ProposedReward { get; set; }
        public string Amount { get; set; }
        public bool IsExclusive { get; set; }
    }
}