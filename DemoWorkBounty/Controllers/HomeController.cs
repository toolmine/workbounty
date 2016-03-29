﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoWorkBounty.Repository;


namespace DemoWorkBounty.Controllers
{
    public class HomeController : Controller
    {

        WorkBountyDBEntities2 entity = new WorkBountyDBEntities2();
        LoginRepo userRepo = new LoginRepo();
        WorkbountyRepo wbRepo = new WorkbountyRepo();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]

        public JsonResult Login(UserInfo id)
        
         {
                try
                {
                    if (ModelState.IsValid)
                    {
                        var user = userRepo.UserLogin(id);
                        Session["UserID"] = user.UserID;
                        Session["Firstname"]=user.FirstName;
                        return Json("Success");
                    }
                    else { return Json("false"); }

                }
                catch (Exception)
                { return Json("false"); }
            }
        


        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Signup(UserInfo u)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(u);
                }
                else
                {
                    ModelState.Clear();
                    return RedirectToAction("AfterLogin");
                }
               
            }
            catch (Exception)
            { }
            return View(u);
        }
       

        public ActionResult AfterLogin()
        {
            int id = Convert.ToInt32(Session["UserID"]);

            var item = wbRepo.getAllItem(id);
            return View(item);
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        public ActionResult DemoSignup()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DemoSignup(UserInfo u)
        {
            try
            {
                
                //if (!ModelState.IsValid)
                //{
                //    ViewBag.Message = "Error";
                //}
                //else
                //{
                //    ViewBag.Message = "Success";

                //}
              

            }
            catch (Exception)
            { }
            return View(u);
        }

       


        public ActionResult mywork()
        {
            return View();
        }

        public ActionResult mybid()
        {
            return View();
        }

        public ActionResult myteam()
        {
            return View();
        }

        public ActionResult profile()
        {
            return View();
        }

        public ActionResult rewards()
        {
            return View();
        }

        public ActionResult addworkitem()
        {
            return View();
        }
    

        public ActionResult Demo()
        {
            return View();

        }

        public ActionResult slackapi()
        {
            return View();

        }
        
        public ActionResult payment()
        {
            return View();

        }

        public ActionResult assigntome()
        {
            return View();

        }

        public ActionResult openworkitem()
        {
            return View();

        }

        public ActionResult workitemDetails()
        {
            return View();
        }
        

    }
}