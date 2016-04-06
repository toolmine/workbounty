using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoWorkBounty.Models
{
    public class AddReward
    {
        public int WorkItemID { get; set; }
        public bool IsRewarded { get; set; }
        public int UserID { get; set; }
        public string ProposedReward { get; set; }
        public string Amount { get; set; }
    }
}