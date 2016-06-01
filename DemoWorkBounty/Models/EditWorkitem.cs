using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoWorkBounty.Models
{
    public class EditWorkitem
    {

        public int WorkitemID { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public int PublishedTo { get; set; }
        public string ProposedReward { get; set; }
        public string Amount { get; set; }
        public int ModifyBy { get; set; }
        public System.DateTime ModifyDateTime { get; set; }
       
    }
}