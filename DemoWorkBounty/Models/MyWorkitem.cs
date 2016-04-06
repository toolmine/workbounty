using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoWorkBounty.Models
{
    public class MyWorkitem
    {
        public List<Workitem> workitem { get; set; }
        public List<WorkitemDistribution> workitemdistribution { get; set; }

        
        public int WorkitemID { get; set; }
        public int TeamID { get; set; }
        public int UserID { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public DateTime DueDate { get; set; }
        public string FirstName { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public string ProposedReward { get; set; }
        public string Amount { get; set; }
        public bool PublishedTo { get; set; }

        


    }
}




 
