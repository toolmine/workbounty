using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DemoWorkBounty.Repository;
namespace DemoWorkBounty.Controllers
{
    public class OpenWorkitemController : ApiController
    {
        static DetailItemRepo repos = new DetailItemRepo();
        private WorkBountyDBEntities3 entity = new WorkBountyDBEntities3();
        public List<Workitem> GetAllWorkitems()
        {
            return entity.Workitems.ToList();
        }

        public List<Workitem> GetAllitems(int id)
        {

            var responce = repos.GetAllitems(id);

            return responce;
        }




    }
}
