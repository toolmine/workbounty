using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace DemoWorkBounty.Controllers
{
    public class DemoShowController : ApiController
    {
        private WorkBountyDBEntities3 db = new WorkBountyDBEntities3();

        // GET api/DemoShow
        public IEnumerable<UserInfo> GetUserInfoes()
        {
            return db.UserInfoes.AsEnumerable();
        }

        // GET api/DemoShow/5
        public UserInfo GetUserInfo(int id)
        {
            UserInfo userinfo = db.UserInfoes.Find(id);
            if (userinfo == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return userinfo;
        }

        // PUT api/DemoShow/5
        public HttpResponseMessage PutUserInfo(int id, UserInfo userinfo)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != userinfo.UserID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(userinfo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/DemoShow
        public HttpResponseMessage PostUserInfo(UserInfo userinfo)
        {
            if (ModelState.IsValid)
            {
                db.UserInfoes.Add(userinfo);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, userinfo);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = userinfo.UserID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/DemoShow/5
        public HttpResponseMessage DeleteUserInfo(int id)
        {
            UserInfo userinfo = db.UserInfoes.Find(id);
            if (userinfo == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.UserInfoes.Remove(userinfo);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, userinfo);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}