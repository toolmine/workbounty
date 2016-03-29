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
    public class ShowitemController : ApiController
    {
        static WorkbountyRepo repo = new WorkbountyRepo();
        static DetailItemRepo repos = new DetailItemRepo();

        public List<OpenWorkItem> GetAllWorkitems()
        {
            return repo.GetAllWorkitems();
        }


        public string AddUserData(UserInfo item)
        {
            try
            { 
            var response = repo.AddUserData(item);
            return response;
                }
            catch(Exception)
            {
                return "error";
            }
        }

       

    }
}
