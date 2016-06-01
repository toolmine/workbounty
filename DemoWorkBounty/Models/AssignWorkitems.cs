using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PagedList.Mvc;

namespace DemoWorkBounty.Models
{
    public class AssignWorkitems
    {
        public int UserID { get; set; }
        public int WorkitemID { get; set; }
         [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public System.DateTime StartDate { get; set; }
         [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public System.DateTime EndDate { get; set; }
        public string FirstName { get; set; }
        public int CreatedBy { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public string ProposedReward { get; set; }
        public string Amount { get; set; }
        public bool IsExclusive { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
        public int AssigntoUserID { get; set; }
    }
}