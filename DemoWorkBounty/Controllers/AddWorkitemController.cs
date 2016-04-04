using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DemoWorkBounty.Repository;
using System.IO;
namespace DemoWorkBounty.Controllers
{
    public class AddWorkitemController : ApiController
    {
        static AddWorkitemRepo repo = new AddWorkitemRepo();
        static AddTeamMemberRepo repos = new AddTeamMemberRepo();
        public string AddWorkitem(Workitem item)
        {
          
            var response = repo.AddWorkitem(item);
            return response;

        }
       

    }
}
