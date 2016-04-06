using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DemoWorkBounty.Repository;
namespace DemoWorkBounty.Controllers
{
    public class DisplayRewardController : ApiController
    {
        static DetailItemRepo repos = new DetailItemRepo();
        static ShowRewardRepo repo = new ShowRewardRepo();

        //public List<WorkItemAssignment> GetAllRewards(int id)
        //{

        //    var responce = repo.GetAllRewards(id);

        //    return responce;
        //}

        //public string AddMemberData(Team memberData)
        //{
        //    var responce = repos.AddMemberData(memberData);
        //    return responce;
        //}


    }
}
