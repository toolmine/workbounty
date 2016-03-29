using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using DemoWorkBounty.Repository;
namespace DemoWorkBounty.Controllers
{
    public class LoginController : ApiController
    {
        static LoginRepo repo = new LoginRepo();

        //public string UserLogin(UserInfo id)
        //{
        //    var response = repo.UserLogin(id);
        //    return response;

        //}


    }
}
