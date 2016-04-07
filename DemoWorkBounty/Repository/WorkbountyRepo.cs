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
        private WorkBountyDBEntities5 entity = new WorkBountyDBEntities5();

        public List<OpenWorkItem> getAllItem(int id)
        {
            try
            {
                int currentUserid = id;
                var teamid = entity.Teams.Where(s => s.UserID == currentUserid).Select(s => s.TeamUserInfoID);
                var data = entity.Workitems.Where(s => s.CreatedBy != currentUserid).Select(s => new OpenWorkItem { WorkItemID = s.WorkitemID, FirstName = s.UserInfo.FirstName, PublishedTo = s.PublishedTo, Title = s.Title, Summary = s.Summary, ProposedReward = s.ProposedReward, Amount = s.Amount }).ToList();
                List<OpenWorkItem> workitemlist = new List<OpenWorkItem>();

                var registereditems = entity.WorkitemRegistrations.Where(s => s.UserID == id).Select(s => new OpenWorkItem { WorkItemID = s.WorkitemID }).ToList();
                foreach (var item in data)
                {
                    if (item.PublishedTo == 0)
                    {
                        workitemlist.Add(entity.Workitems.Where(s => s.WorkitemID == item.WorkItemID).Select(s => new OpenWorkItem { WorkItemID = s.WorkitemID, FirstName = s.UserInfo.FirstName, Title = s.Title, Summary = s.Summary, ProposedReward = s.ProposedReward, Amount = s.Amount }).FirstOrDefault());
                    }
                    else
                    {
                        foreach (var items in teamid)
                        {
                            if (item.PublishedTo == items)
                            {

                                workitemlist.Add(entity.Workitems.Where(s => s.WorkitemID == item.WorkItemID).Select(s => new OpenWorkItem { WorkItemID = s.WorkitemID, FirstName = s.UserInfo.FirstName, Title = s.Title, Summary = s.Summary, ProposedReward = s.ProposedReward, Amount = s.Amount }).FirstOrDefault());

                            }
                        }
                    }
                }

                workitemlist.RemoveAll(x => registereditems.Any(y => y.WorkItemID == x.WorkItemID));
                return workitemlist;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<MyWorkitemAssignment> GetMyWorkitem(int id)
        {
            List<WorkitemRegistration> item = new List<WorkitemRegistration>();
            int currentId = id;
            var name = entity.WorkitemRegistrations.Where(w => w.UserID == currentId).Select(s => s.Workitem.UserInfo.FirstName).FirstOrDefault();
            var data = entity.WorkitemRegistrations.Where(s => s.UserID == currentId).Select(s => new MyWorkitemAssignment { WorkItemID = s.WorkitemID, Title = s.Workitem.Title, StartDate = s.Workitem.StartDate, EndDate = s.Workitem.DueDate, FirstName = name, ProposedReward = s.Workitem.ProposedReward, Amount = s.Workitem.Amount }).ToList();

            return data;
        }



        public List<MyWorkitem> GetMyWorkitems()
        {
            var status = entity.WorkitemDistributions.Where(s => s.Workitem.Status);
            var data = entity.WorkitemDistributions.Select(s => new MyWorkitem { Title = s.Workitem.Title, StartDate = s.Workitem.StartDate, EndDate = s.Workitem.DueDate, FirstName = s.UserInfo.FirstName, ProposedReward = s.Workitem.ProposedReward }).ToList();
            return data;
        }

        public UserInfo AddUserData(UserInfo item)
        {
            try
            {
                
                    var v = entity.UserInfoes.Where(a => a.Email.Equals(item.Email)).FirstOrDefault();
                    if (v == null)
                    {
                        entity.UserInfoes.Add(item);
                        entity.SaveChanges();
                        return item;
                    }
                   
                
               else
                {
                    return null;
                }
            }
            catch(Exception)
            {
                return null;
            }
        }

        public List<MyWorkitem> ItemsIWantDone(int id)
        {
            List<Workitem> item = new List<Workitem>();

            var data2 = from u in entity.Workitems.Where(s => s.CreatedBy == id)
                        join b in entity.WorkitemDistributions
                        on u.WorkitemID equals b.WorkitemID
                        into userArticles
                        from ua in userArticles.DefaultIfEmpty()
                        select new MyWorkitem { WorkitemID = u.WorkitemID, Title = u.Title, FirstName = ua.UserInfo.FirstName, ProposedReward = u.ProposedReward, StartDate = u.StartDate, EndDate = u.DueDate };


            return data2.ToList();
        }

        public List<WorkitemRegistration> Applied(int id)
        {
            List<WorkitemRegistration> item = new List<WorkitemRegistration>();
            var getDataforAssignWorkitem = entity.WorkitemDistributions.Where(s => s.WorkitemID == id).FirstOrDefault();

            if (getDataforAssignWorkitem == null)
            {
                var data = entity.WorkitemRegistrations.Where(s => s.WorkitemID == id && s.IsExclusive == true).ToList();
                return data;
            }
            else
            {
                return null;
            }
           
        }
        public List<ViewDocuments> ShowDocument(int id)
        {
            List<MyWorkitemAssignment> item = new List<MyWorkitemAssignment>();
            var getDataForIsRewarded = entity.WorkItemAssignments.Where(s => s.WorkItemID == id).Select(s => s.IsRewarded == true).FirstOrDefault();
            if (getDataForIsRewarded == null)
            {
                var data = entity.WorkItemAssignments.Where(s => s.WorkItemID == id).Select(s => new ViewDocuments { WorkItemID = s.WorkItemID, UserID = s.UserID, Title = s.Workitem.Title, Summary = s.Workitem.Summary, FirstName = s.UserInfo.FirstName, SubmissionDateTime = s.SubmissionDateTime, SubmissionPath = s.SubmissionPath }).ToList();
                return data;
            }
            else
            {

                return null;
            }
        }

        public List<Workitem> GetAllitemsDone(int id)
        {
            List<WorkitemRegistration> item = new List<WorkitemRegistration>();
            var data = entity.Workitems.Where(s => s.WorkitemID == id).ToList();
            return data;
        }

        public string ApplyReward(AddReward id)
        {
            try
            {
                WorkItemAssignment item = entity.WorkItemAssignments.Where(s => s.WorkItemID == id.WorkItemID && s.UserID == id.UserID).First();
                item.IsRewarded = true;
                entity.SaveChanges();
                return "Success";

            }
            catch (Exception)
            {
                return "Error";
            }

        }


        public string AddAssignData(int id)
        {
            if (ModelState.IsValid)
            {
                entity.SaveChanges();
                return "Success";
            }
            return "Error";
        }


        public string WorkitemDistribution(WorkitemDistribution Data)
        {
            if (ModelState.IsValid)
            {
                entity.WorkitemDistributions.Add(Data);
                entity.SaveChanges();
                return "Success";

            }
            return "Error";
        }

      
       

        public List<ViewDocuments> ShowExclusiveDocument(int id)
        {
            List<MyWorkitemAssignment> item = new List<MyWorkitemAssignment>();
            var data = entity.WorkItemAssignments.Where(s => s.WorkItemID == id).Select(s => new ViewDocuments { Title = s.Workitem.Title, Summary = s.Workitem.Summary, FirstName = s.UserInfo.FirstName, SubmissionDateTime = s.SubmissionDateTime, SubmissionPath = s.SubmissionPath }).ToList();
            return data;
        }

    }
}