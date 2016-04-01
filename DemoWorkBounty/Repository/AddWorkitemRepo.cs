using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DemoWorkBounty.Repository
{
    public class AddWorkitemRepo : ApiController
    {
        private WorkBountyDBEntities5 entity = new WorkBountyDBEntities5();
        public string AddWorkitem(Workitem item)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    entity.Workitems.Add(item);
                    entity.SaveChanges();
                    return "Data Successfully saved";
                }
                else
                {
                    return "Error";
                }
            }

            catch (Exception)
            {
                return "error";
            }
        }
    }
}