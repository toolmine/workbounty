using DemoWorkBounty.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DemoWorkBounty.Repository
{
    public class WorkbountyRepo : ApiController
    {
        private WorkBountyDBEntities4 entity = new WorkBountyDBEntities4();
        
        public List<OpenWorkItem> getAllItem(int id)
        {
            try
            {
                int currentUserid = id;

                var data = entity.Workitems.Where(s => s.CreatedBy != currentUserid).Select(s => new OpenWorkItem { WorkItemID = s.WorkitemID, FirstName = s.UserInfo.FirstName, Title = s.Title, Summary = s.Summary, ProposedReward = s.ProposedReward, Amount = s.Amount }).ToList();
                return data;
            }
            catch(Exception)
            {
                return null;
            }

        }
        
        public List<OpenWorkItem> GetAllWorkitems()
        {
            List<Workitem> item=new List<Workitem>();
            int id = Convert.ToInt16(System.Web.HttpContext.Current.Session["UserID"]);
            var data = entity.Workitems.Where(s=>s.CreatedBy!=id).Select(s => new OpenWorkItem { WorkItemID = s.WorkitemID, FirstName = s.UserInfo.FirstName, Title = s.Title, Summary = s.Summary, ProposedReward = s.ProposedReward, Amount = s.Amount }).ToList();

          return data;
        }

        public List<MyWorkitem> GetMyWorkitems()
        {
            var status = entity.WorkitemDistributions.Where(s => s.Workitem.Status);
            var data = entity.WorkitemDistributions.Select(s => new MyWorkitem { Title = s.Workitem.Title, StartDate = s.Workitem.StartDate, EndDate = s.Workitem.DueDate, FirstName = s.UserInfo.FirstName, ProposedReward = s.Workitem.ProposedReward }).ToList();

            return data;
        }

        public string AddUserData(UserInfo item)
        {
            if(ModelState.IsValid)
            {
                var v = entity.UserInfoes.Where(a => a.Email.Equals(item.Email)).FirstOrDefault();
                
                if(v==null)
                { 
                entity.UserInfoes.Add(item);
                entity.SaveChanges();
                return "Success";
                //System.Web.HttpContext.Current.Session["LogedUserID"] = item.UserID;
                //System.Web.HttpContext.Current.Session["LogedUserFirstName"] = item.FirstName.ToString();
                }
                
            }
            return "Error";
            
        }



        public List<Workitem> ItemsIWantDone()
        {
            List<Workitem> item = new List<Workitem>();
            int id = Convert.ToInt16(System.Web.HttpContext.Current.Session["UserID"]);
            var data = entity.Workitems.Where(s => s.CreatedBy == id).Select(s => new Workitem { Title = s.Title, Summary = s.Summary, ProposedReward = s.ProposedReward, Amount = s.Amount,StartDate=s.StartDate, DueDate=s.DueDate  }).ToList();

            return data;
        }


        

    }
}