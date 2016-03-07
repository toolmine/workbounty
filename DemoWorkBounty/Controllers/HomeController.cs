using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoWorkBounty.Models;


namespace DemoWorkBounty.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login u)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return RedirectToAction("AfterLogin");
                }
               
            }
            catch (Exception)
            { }
            return View();
        }


        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Signup(Register u)
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
        public ActionResult DemoSignup(Register u)
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

        public ActionResult AfterLogin()
        {
            return View();
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
    }
}