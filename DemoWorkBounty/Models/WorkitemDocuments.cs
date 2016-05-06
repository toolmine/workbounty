using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DemoWorkBounty.Models
{
    public class WorkitemDocuments
    {
        public int WorkItemAssignmentID { get; set; }
        public string FirstName { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public int WorkItemID { get; set; }
        public bool IsExclusive { get; set; }
          [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public System.DateTime DueDate { get; set; }
         public string SubmissionPath { get; set; }
       [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public System.DateTime SubmissionDateTime { get; set; }
        public bool IsRewarded { get; set; }
        public int UserID { get; set; }
    }
}