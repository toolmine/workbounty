using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DemoWorkBounty.Repository;
using DemoWorkBounty.Models;

namespace DemoWorkBounty.Controllers
{
    public class MyWorkitemController : ApiController
    {
        static WorkbountyRepo repo = new WorkbountyRepo();
        public List<MyWorkitem> GetMyWorkitems()
        {
            return repo.GetMyWorkitems();
        }

         
        public string Workitemdistribution(WorkitemDistribution Data)
        {
            var responce = repo.WorkitemDistribution(Data);
            return responce;
        }
    }

}
