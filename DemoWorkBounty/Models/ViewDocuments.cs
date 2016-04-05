using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoWorkBounty.Models
{
    public class ViewDocuments
    {
        public string FirstName { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public int WorkItemID { get; set; }
        public bool IsExclusive { get; set; }
        public System.DateTime DueDate { get; set; }
        public string SubmissionPath { get; set; }
        public System.DateTime SubmissionDateTime { get; set; }
        public bool IsRewarded { get; set; }
        public int UserID { get; set; }
    }
}