using DemoWorkBounty;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DemoWorkBounty.Models
{
    public class AddWorkitems
    {
        public List<Workitem> workitem { get; set; }
        public List<WorkitemDistribution> workitemdistribution { get; set; }

        public int WorkitemID { get; set; }
        public int TeamID { get; set; }
        public int UserID { get; set; }
      [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public System.DateTime StartDate { get; set; }
         [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public System.DateTime EndDate { get; set; }
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public System.DateTime DueDate { get; set; }
        public string FirstName { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public string ProposedReward { get; set; }
        public string Amount { get; set; }
        public bool PublishedTo { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
        public bool IsRewarded { get; set; }
        public string Remarks { get; set; }

    }
}