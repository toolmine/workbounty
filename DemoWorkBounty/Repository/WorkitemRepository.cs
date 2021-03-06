﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DemoWorkBounty.Models;
using System.IO;
namespace DemoWorkBounty.Repository
{
    public class WorkitemRepository : ApiController
    {
        private WorkBountyDBEntities entity = new WorkBountyDBEntities();

        public string AddWorkitem(Workitem addWorkitemData)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    entity.Workitems.Add(addWorkitemData);
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
        public List<Workitem> GetWorkDetails(int currentWorkitemID)
        {
            var assignWorkitemData = entity.Workitems.Where(a => a.WorkitemID.Equals(currentWorkitemID)).ToList();

            if (assignWorkitemData == null)
            {
                return null;
            }

            return assignWorkitemData;
        }

        [HttpPost]
        public string UserRegisterForWorkitem(WorkitemRegistration dataForWorkitemRegistration)
        {
            try
            {
                entity.WorkitemRegistrations.Add(dataForWorkitemRegistration);
                entity.SaveChanges();
                return "Success";
            }

            catch (Exception)
            {

                return "Error";
            }
        }

        [HttpPost]
        public string RemoveFavouriteWorkitem(WorkitemRegistration dataForWorkitemRegistration)
        {
            try
            {
                var Fav = entity.WorkitemRegistrations.Where(s => s.WorkitemID == dataForWorkitemRegistration.WorkitemID && s.IsFavourite == true).FirstOrDefault();
                entity.WorkitemRegistrations.Remove(Fav);
                entity.SaveChanges();
                return "Success";
            }

            catch (Exception)
            {
                return "Error";
            }
        }

        public string AddMemberDetails(Team memberData)
        {
            try
            {
                entity.Teams.Add(memberData);
                entity.SaveChanges();
                return "Success";
            }
            catch (Exception)
            {
                return "Error";
            }
        }



        public string UpdateWorkitems(WorkItemAssignment data)
        {
            try
            {
                entity.WorkItemAssignments.Add(data);
                entity.SaveChanges();
                return "Success";
            }
            catch (Exception)
            {
                return "Error";
            }
        }
        public List<OpenWorkitems> GetAllWorkitems(int currentUserID)
        {
            try
            {
                var getTeamID = entity.Teams.Where(s => s.UserID == currentUserID).Select(s => s.TeamUserInfoID);
                var getWorkitemData = entity.Workitems.Where(s => s.CreatedBy != currentUserID).Select(s => new OpenWorkitems { WorkitemID = s.WorkitemID, FirstName = s.UserInfo.FirstName, PublishedTo = s.PublishedTo, Title = s.Title, Summary = s.Summary, ProposedReward = s.ProposedReward, Amount = s.Amount }).ToList();
                List<OpenWorkitems> workitemlist = new List<OpenWorkitems>();
                List<OpenWorkitems> workitemlist2 = new List<OpenWorkitems>();

                var favourites = entity.WorkitemRegistrations.Where(s => s.UserID == currentUserID && s.IsFavourite == true).Select(s => new OpenWorkitems { WorkitemID = s.WorkitemID }).ToList();
                var registereditems = entity.WorkitemRegistrations.Where(s => s.UserID == currentUserID && s.IsFavourite == false).Select(s => new OpenWorkitems { WorkitemID = s.WorkitemID }).ToList();
                foreach (var getUserData in getWorkitemData)
                {
                    if (getUserData.PublishedTo == 0)
                    {
                        workitemlist.Add(entity.Workitems.Where(s => s.WorkitemID == getUserData.WorkitemID).Select(s => new OpenWorkitems { WorkitemID = s.WorkitemID, FirstName = s.UserInfo.FirstName, Title = s.Title, Summary = s.Summary, ProposedReward = s.ProposedReward, Amount = s.Amount, EndDate = s.DueDate, CreatedDateTime = s.CreatedDateTime, DocumentFilePath = s.DocumentFilePath }).FirstOrDefault());
                    }
                    else
                    {
                        foreach (var getUserTeamID in getTeamID)
                        {
                            if (getUserData.PublishedTo == getUserTeamID)
                            {
                                workitemlist.Add(entity.Workitems.Where(s => s.WorkitemID == getUserData.WorkitemID).Select(s => new OpenWorkitems { WorkitemID = s.WorkitemID, FirstName = s.UserInfo.FirstName, Title = s.Title, Summary = s.Summary, ProposedReward = s.ProposedReward, Amount = s.Amount, CreatedDateTime = s.CreatedDateTime, EndDate = s.DueDate, DocumentFilePath = s.DocumentFilePath }).FirstOrDefault());
                            }
                        }
                    }
                }
                workitemlist.RemoveAll(x => registereditems.Any(y => y.WorkitemID == x.WorkitemID));

                foreach (var favoriteData in favourites)
                {
                    workitemlist.Where(a => a.WorkitemID == favoriteData.WorkitemID).Select(q => q.IsFavourite = true).FirstOrDefault();
                }
                workitemlist = workitemlist.OrderByDescending(s => s.CreatedDateTime).ToList();

                foreach (var item in workitemlist)
                {
                    if (item.EndDate > DateTime.Today && entity.WorkItemAssignments.Where(q => q.WorkItemID == item.WorkitemID && q.IsRewarded == true).FirstOrDefault() == null)
                    {
                        workitemlist2.Add(item);
                    }
                }
                var favourite = workitemlist2.Where(x => x.IsFavourite == true).ToList();
                var notfavourite = workitemlist2.Where(x => x.IsFavourite == false).ToList();
                favourite.AddRange(notfavourite);
                return favourite;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<AssignWorkitems> GetCurrentWorkitem(int currentUserID)
        {
            DateTime currentDate = DateTime.Now;
            var WorkitemRegisteredUserID = entity.WorkitemRegistrations.Where(s => s.UserID == currentUserID && s.IsRegistered == true);
            var getCurrentWorkitemData = new List<AssignWorkitems>();
            var items = WorkitemRegisteredUserID.Where(s => s.Workitem.DueDate >= currentDate).Select(s => s.WorkitemID).ToList();
            foreach (var workItemID in items)
            {
                var assign = 0;
                if (entity.WorkItemAssignments.Where(s => s.WorkItemID == workItemID && s.IsRewarded == true).FirstOrDefault() != null)
                {
                    assign = 2;
                }
                else
                {
                    if (entity.WorkitemDistributions.Where(s => s.WorkitemID == workItemID && s.UserID == currentUserID).ToList().Count() != 0 || WorkitemRegisteredUserID.Where(s => s.WorkitemID == workItemID && s.IsExclusive == false).ToList().Count() != 0)
                    {
                        assign = 1;
                    }
                }
                var item = entity.WorkitemRegistrations.Where(s => s.UserID == currentUserID && s.WorkitemID == workItemID).Select(s => new AssignWorkitems { WorkitemID = s.WorkitemID, Title = s.Workitem.Title, StartDate = s.Workitem.StartDate, EndDate = s.Workitem.DueDate, FirstName = s.Workitem.UserInfo.FirstName, ProposedReward = s.Workitem.ProposedReward, Amount = s.Workitem.Amount, CreatedDateTime = s.Workitem.CreatedDateTime, AssigntoUserID = assign }).FirstOrDefault();
                getCurrentWorkitemData.Add(item);
            }
            getCurrentWorkitemData.OrderByDescending(s => s.CreatedDateTime).ToList();
            return getCurrentWorkitemData;
        }

        public List<AddWorkitems> GetCurrentWorkitem()
        {
            var status = entity.WorkitemDistributions.Where(s => s.Workitem.Status);
            var getCurrentWorkitemData = entity.WorkitemDistributions.Select(s => new AddWorkitems { Title = s.Workitem.Title, StartDate = s.Workitem.StartDate, EndDate = s.Workitem.DueDate, FirstName = s.UserInfo.FirstName, ProposedReward = s.Workitem.ProposedReward }).ToList();
            return getCurrentWorkitemData;
        }

        public List<UpdateWorkitems> ShowCurrentWorkitems(int currentWorkitemID)
        {
            var IsRewardedWorkitemData = entity.WorkItemAssignments.Where(s => s.WorkItemID == currentWorkitemID && s.IsRewarded == true).FirstOrDefault();
            List<UpdateWorkitems> updateWorkitemData = new List<UpdateWorkitems>();
            if (IsRewardedWorkitemData == null)
            {
                updateWorkitemData.Add(entity.WorkitemRegistrations.Where(s => s.WorkitemID == currentWorkitemID).Select(s => new UpdateWorkitems { Title = s.Workitem.Title, Summary = s.Workitem.Summary, WorkItemID = s.WorkitemID }).FirstOrDefault());
            }
            return updateWorkitemData;

        }

        public List<AddWorkitems> ItemsIWantDone(int currentWorkitemID)
        {
            var status = entity.WorkItemAssignments.Where(q => q.IsRewarded == true).ToList();
            var status2 = entity.WorkItemAssignments.Where(q => q.IsRewarded == true).Select(a => a.WorkItemID).ToList();
            var getListofAssignUser = from u in entity.Workitems.Where(s => s.CreatedBy == currentWorkitemID)
                                      join b in entity.WorkitemDistributions
                                      on u.WorkitemID equals b.WorkitemID
                                      into userArticles
                                      from ua in userArticles.DefaultIfEmpty()
                                      select new AddWorkitems { WorkitemID = u.WorkitemID, Title = u.Title, FirstName = ua.UserInfo.FirstName, ProposedReward = u.ProposedReward, StartDate = u.StartDate, EndDate = u.DueDate, CreatedDateTime = u.CreatedDateTime };
            var getListofAssignUserList = getListofAssignUser.ToList();
            List<AddWorkitems> itemlist = new List<AddWorkitems>();
            List<WorkItemAssignment> assignData = new List<WorkItemAssignment>();
            foreach (var z in getListofAssignUserList)
            {
                var temp = entity.WorkItemAssignments.Where(a => a.WorkItemID == z.WorkitemID && a.IsRewarded == true).FirstOrDefault();
                if (temp != null)
                {
                    assignData.Add(temp);
                }
            }
            if (assignData.Any())
            {
                var getWorkitemStatus = from u in assignData
                                        join o in getListofAssignUserList on u.WorkItemID equals o.WorkitemID
                                        into completeditems
                                        from ci in completeditems.DefaultIfEmpty()
                                        select new AddWorkitems { WorkitemID = ci.WorkitemID, Title = ci.Title, FirstName = ci.FirstName, ProposedReward = ci.ProposedReward, StartDate = ci.StartDate, EndDate = ci.EndDate, CreatedDateTime = ci.CreatedDateTime, Status = "Completed", Remarks = entity.Workitems.Where(q => q.WorkitemID == ci.WorkitemID).Select(b => b.Remarks).FirstOrDefault() };
                var getWorkitemStatusList = getWorkitemStatus.ToList();
                getListofAssignUserList.RemoveAll(x => status.Any(y => y.WorkItemID == x.WorkitemID));
                itemlist = getListofAssignUserList.Union(getWorkitemStatusList).ToList();
                itemlist = itemlist.OrderByDescending(s => s.CreatedDateTime).ToList();
                var getWorkitemData = itemlist.ToList();
                return getWorkitemData;
            }
            else
            {
                getListofAssignUserList = getListofAssignUserList.OrderByDescending(s => s.CreatedDateTime).ToList();
                return getListofAssignUserList;
            }
        }

        public List<WorkitemRegistration> AppliedWorkitems(int currentWorkitemID)
        {
            var getDataforAssignWorkitem = entity.WorkitemDistributions.Where(s => s.WorkitemID == currentWorkitemID).FirstOrDefault();

            if (getDataforAssignWorkitem == null)
            {
                var getAssignWorkitemData = entity.WorkitemRegistrations.Where(s => s.WorkitemID == currentWorkitemID && s.IsExclusive == true).ToList();
                return getAssignWorkitemData;
            }
            else
            {
                return null;
            }
        }

        public List<WorkitemDocuments> UserUploadDocument(int currentWorkitemID, int currentUserID)
        {
            var getListofUploadDocuments = entity.WorkItemAssignments.Where(s => s.WorkItemID == currentWorkitemID && s.UserID == currentUserID).Select(s => new WorkitemDocuments { WorkItemID = s.WorkItemID, UserID = s.UserID, Title = s.Workitem.Title, Summary = s.Workitem.Summary, SubmissionDateTime = s.SubmissionDateTime, SubmissionPath = s.SubmissionPath, WorkItemAssignmentID = s.WorkItemAssignmentID }).ToList();
            return getListofUploadDocuments;
        }

        public List<WorkitemDocuments> ShowDocument(int id)
        {

            var getDataForSubmitData = entity.WorkItemAssignments.Where(s => s.WorkItemID == id).Select(s => s.SubmissionPath == null).FirstOrDefault();
            var getDataForIsRewarded = entity.WorkItemAssignments.Where(s => s.WorkItemID == id).Select(s => s.IsRewarded == true).FirstOrDefault();
            if (getDataForIsRewarded == false)
            {
                var getListofUserAppliedForWorkitem = entity.WorkItemAssignments.Where(s => s.WorkItemID == id).Select(s => new WorkitemDocuments { WorkItemAssignmentID = s.WorkItemAssignmentID, WorkItemID = s.WorkItemID, UserID = s.UserID, Title = s.Workitem.Title, Summary = s.Workitem.Summary, FirstName = s.UserInfo.FirstName, SubmissionDateTime = s.SubmissionDateTime, SubmissionPath = s.SubmissionPath }).ToList();
                return getListofUserAppliedForWorkitem;
            }
            else
            {
                return null;
            }
        }

        public List<WorkitemDocuments> CheckDocument(int id)
        {

            var getDataForSubmitData = entity.WorkItemAssignments.Where(s => s.WorkItemID == id).Select(s => s.SubmissionPath).FirstOrDefault();

            if (getDataForSubmitData != null)
            {
                var getListofUserAppliedForWorkitem = entity.WorkItemAssignments.Where(s => s.WorkItemID == id).Select(s => new WorkitemDocuments { WorkItemID = s.WorkItemID, UserID = s.UserID, Title = s.Workitem.Title, Summary = s.Workitem.Summary, FirstName = s.UserInfo.FirstName, SubmissionDateTime = s.SubmissionDateTime, SubmissionPath = s.SubmissionPath }).ToList();
                return getListofUserAppliedForWorkitem;
            }
            else
            {
                return null;
            }
        }

        public List<Workitem> GetAllitemsDone(int currentWorkitemID)
        {
            List<WorkitemRegistration> getListofAppliedWorkitem = new List<WorkitemRegistration>();
            var getListofAssignWorkitem = entity.Workitems.Where(s => s.WorkitemID == currentWorkitemID).ToList();
            var rewarded = entity.WorkItemAssignments.Where(s => s.IsRewarded == true && s.WorkItemID == currentWorkitemID).ToList();
            if (rewarded.Count != 0)
            {
                getListofAssignWorkitem.Any(s => s.Status = false);
            }
            return getListofAssignWorkitem;
        }

        public string ApplyReward(Rewards id)
        {
            try
            {
                using (WorkBountyDBEntities entities = new WorkBountyDBEntities())
                {
                    entities.Configuration.ValidateOnSaveEnabled = false;
                    string remarks = id.Remarks.ToString();
                    Workitem remark = new Workitem() { WorkitemID = id.WorkItemID, Remarks = id.Remarks };
                    entities.Workitems.Attach(remark);
                    entities.Entry(remark).Property(u => u.Remarks).IsModified = true;
                    entities.SaveChanges();
                }
                List<WorkItemAssignment> checkUploadedWorkitem = entity.WorkItemAssignments.Where(s => s.WorkItemID == id.WorkItemID && s.UserID == id.UserID).ToList();
                foreach (var data in checkUploadedWorkitem)
                {
                    WorkItemAssignment item = entity.WorkItemAssignments.Where(s => s.WorkItemID == id.WorkItemID && s.UserID == id.UserID).FirstOrDefault();
                    item.IsRewarded = true;
                    entity.SaveChanges();
                }
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

        public string WorkitemDistribution(WorkitemDistribution getWorkitemData)
        {
            if (ModelState.IsValid)
            {
                entity.WorkitemDistributions.Add(getWorkitemData);
                entity.SaveChanges();
                return "Success";
            }
            return "Error";
        }

        public List<WorkitemDocuments> ShowExclusiveDocument(int currentWorkitemID)
        {
            var showExclusiveData = entity.WorkItemAssignments.Where(s => s.WorkItemID == currentWorkitemID).Select(s => new WorkitemDocuments { Title = s.Workitem.Title, Summary = s.Workitem.Summary, FirstName = s.UserInfo.FirstName, SubmissionDateTime = s.SubmissionDateTime, SubmissionPath = s.SubmissionPath }).ToList();
            return showExclusiveData;
        }

        public List<Workitem> SearchWorkitems(string searchValue)
        {
            var getSearchWorkitemResults = entity.Workitems.Where(s => s.Title.StartsWith(searchValue)).ToList();
            return getSearchWorkitemResults;
        }

        public List<Team> SelectTeam(int currentUserID)
        {
            List<Team> selectedteamData = new List<Team>();
            selectedteamData.Add(new Team { TeamName = "Public", TeamUserInfoID = 0 });
            foreach (var item in entity.Teams)
            {
                if (item.UserID == currentUserID)
                {
                    selectedteamData.Add(entity.Teams.Where(s => s.TeamID == item.TeamID).FirstOrDefault());
                }
            }
            return selectedteamData;
        }

        public string EditWorkitem(Workitem id)
        {
            try
            {
                using (var entities = new WorkBountyDBEntities())
                {
                    var getWorkitemData = entities.Workitems.Where(x => x.WorkitemID==id.WorkitemID).ToList();
                    getWorkitemData.ForEach(a =>
                    {
                        if(id.Amount!=null) a.Amount = id.Amount;
                         a.ModifyBy = id.ModifyBy;
                        if (id.ModifyDateTime != null) a.ModifyDateTime = id.ModifyDateTime;
                         a.PublishedTo = id.PublishedTo;
                        if (id.Summary != null) a.Summary = id.Summary;
                        if (id.Title != null) a.Title = id.Title;
                        if (id.ProposedReward != "undefined") a.ProposedReward = id.ProposedReward;
                    }
                 
               );
                    WorkitemStatu workitemstatusData = new WorkitemStatu();
                    string statusDescription = "Update";
                    workitemstatusData.StatusDescription = statusDescription;
                    workitemstatusData.WorkitemID = id.WorkitemID;
                    entities.WorkitemStatus.Add(workitemstatusData);
                    entities.SaveChanges();
                    WorkitemHistory workitemHistoryData = new WorkitemHistory();
                    workitemHistoryData.WorkitemID = id.WorkitemID;
                    workitemHistoryData.UpdatedBy = id.ModifyBy;
                    workitemHistoryData.UpdatedDateTIme = DateTime.Now;
                    var getWorkitemStatusID = entities.WorkitemStatus.Where(s => s.WorkitemID == id.WorkitemID).Select(s => s.WorkitemStatusID).FirstOrDefault();
                    workitemHistoryData.WorkitemStatusID = getWorkitemStatusID;
                    entities.WorkitemHistories.Add(workitemHistoryData);
                    entities.SaveChanges();

                }
               
                return "Success";
            }
            catch (Exception)
            {
                return "Error";
            }
        }


    }
}