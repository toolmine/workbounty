using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DemoWorkBounty.Models;


namespace DemoWorkBounty.Repository
{
    public class SearchMyDataRepo : ApiController
    {
        private WorkBountyDBEntities5 entity = new WorkBountyDBEntities5();

        public List<Workitem> GetItemById(string id)
        {

            try
            {

                var data = entity.Workitems.Where(s => s.Title.Contains(id)).ToList();
                return data;
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }


        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}