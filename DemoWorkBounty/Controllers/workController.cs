﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoWorkBounty.Repository;
using System.IO;
using DemoWorkBounty.Models;

namespace DemoWorkBounty.Controllers
{
    public class workController : Controller
    {
        WorkBountyDBEntities5 entity = new WorkBountyDBEntities5();
        DetailItemRepo repo = new DetailItemRepo();
        WorkbountyRepo wbrepo = new WorkbountyRepo();
        public ActionResult updateTask()
        {
            return View();
        }

        public ActionResult detailWorkitem(int id)
        {


            var item = repo.GetAllitems(id);

            return View(item);
        }


        public ActionResult addWorkitem()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddDocument(int id)
        {

            var data = repo.ShowMyWorkitems(id);
            return View(data);
        }
        [HttpPost]
        public JsonResult AddDocument(FormCollection data, HttpPostedFileBase myFile)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json("Error");
                }
                else
                {

                    WorkItemAssignment assignData = new WorkItemAssignment();
                    int Workid = Convert.ToInt32(data["Workid"].ToString());
                    
                    var fileName = Path.GetFileName(myFile.FileName);
                        var path = Path.Combine(Server.MapPath("~/work/Download/"), fileName);
                        myFile.SaveAs(path);
                        assignData.WorkItemID = Workid;
                        assignData.UserID = Convert.ToInt32(Session["UserID"]);
                        assignData.IsRewarded = false;
                        assignData.SubmissionDateTime = DateTime.Now;
                        assignData.SubmissionPath = fileName;
                    var putAssignData = repo.UpdateWorkitems(assignData);
                    return Json("Success");
                }

            }
            catch (Exception)
            {
                return Json("Error");
            }
       
        }
      

    
        public ActionResult ViewDocument(int id)
        {
            var dataForOpen = wbrepo.ShowDocument(id);
            

            ViewBag.dataForOpen = dataForOpen;
       
            return View();
        }

        public FileResult Download(int id)
        {

            int fid = id;
         
            var files = entity.WorkItemAssignments.Where(s => s.UserID == id).Select(s => s.SubmissionPath).FirstOrDefault();
            string fileName = entity.WorkItemAssignments.Where(s => s.UserID == id).Select(s => s.SubmissionPath).FirstOrDefault();
            

            return File(files, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
    }
}
