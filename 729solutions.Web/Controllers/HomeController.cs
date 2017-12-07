using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _729solutions.Web.Models;

namespace _729solutions.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        //public ActionResult Index(string UserEmail)
        {
            //WcfServiceReference.Service1Client WcfService = new WcfServiceReference.Service1Client();
            //var lst = WcfService.getUsersbyEmail(UserEmail);
            //User usr = new User();
            //usr.Id = lst.Id;
            //usr.FirstName = lst.FirstName;
            //usr.LastName = lst.LastName;
            //usr.LastName = lst.LastName;
            //usr.UserName = lst.UserName;
            //return View(lst);

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}