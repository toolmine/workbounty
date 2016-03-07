using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoWorkBounty.Models
{
    public class viewWorkItem
    {
        public List<State> StateModel { get; set; }
        public SelectList FilteredCity { get; set; }


        public class State
        {
            public int Id { get; set; }
            public string StateName { get; set; }
        }
        public class City
        {
            public int Id { get; set; }
            public int StateId { get; set; }
            public string ItemName { get; set; }
            public string Email { get; set; }

        }
    }
}