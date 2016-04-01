using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace DemoWorkBounty.Repository
{
    public class LoginRepo : ApiController
    {
        private WorkBountyDBEntities4 entity = new WorkBountyDBEntities4();

        public UserInfo UserLogin(UserInfo id)
        {
            try
            {


                var checkData = entity.UserInfoes.Where(a => a.Email.Equals(id.Email) && a.Password.Equals(id.Password)).FirstOrDefault();

                if (checkData == null)
                {
                    return checkData;
                }
                else
                {

                    return checkData;
                    //System.Web.HttpContext.Current.Session["LogedUserID"] = email.UserID.ToString();
                    //System.Web.HttpContext.Current.Session["LogedUserID"] = email.UserID;
                    //System.Web.HttpContext.Current.Session["LogedUserFirstname"] = id.FirstName.ToString();


                    //sessionStorage.setItem('key3', System.Web.HttpContext.Current.Session["LogedUserID"]);

                }


            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
