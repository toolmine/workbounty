using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoWorkBounty.Repository;
using DemoWorkBounty.Models;


namespace DemoWorkBounty.Controllers
{
    public class HomeController : Controller
    {
        SeeRewardRepo repo = new SeeRewardRepo();
        WorkBountyDBEntities5 entity = new WorkBountyDBEntities5();
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
                    Session["FirstName"] = user.FirstName;
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

            ViewBag.item = item;

            return View();
        }

        public ActionResult myteam()
        {
            MyTeamRepo teamrepo = new MyTeamRepo();
            int id = Convert.ToInt32(Session["UserID"]);
            var item = teamrepo.getAllItem(id);
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



        public ActionResult profile()
        {
            return View();
        }

        public ActionResult rewards()
        {
            return View();
        }

        [HttpPost]
        public JsonResult rewards(int id)
        {
            SeeRewardRepo repo = new SeeRewardRepo();
            var Data = repo.GetAllRewards(id);
            return Json(Data, JsonRequestBehavior.AllowGet);
        }


        public ActionResult addworkitem()
        {
            var id1 = 1;
            var selected = (from tea in entity.Teams
                            where tea.UserID == id1
                            select tea);

            ViewBag.TeamName1 = new SelectList(selected, "TeamID", "TeamName");
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
        public ActionResult Search()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Search(string id)
        {
            SearchMyDataRepo repo = new SearchMyDataRepo();
            var searchData = repo.GetItemById(id.ToString());
            List<MyWorkitem> wi = new List<MyWorkitem>();
            foreach (var data in searchData)
            {
                MyWorkitem mwi = new MyWorkitem() { Title = data.Title, StartDate=data.StartDate, DueDate = data.DueDate, ProposedReward = data.ProposedReward, Amount = data.Amount };
                wi.Add(mwi);
            }
            return Json(wi, JsonRequestBehavior.AllowGet);
        }

    }
}