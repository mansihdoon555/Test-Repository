using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _729solutions.Web.Models;
using PagedList;
using PagedList.Mvc;
using System.Net;
using System.Data.Entity;

namespace _729solutions.Web.Controllers
{
    public class UserController : Controller
    {
        private StnSolutionsEntities db = new StnSolutionsEntities();
        // GET: User
        public ActionResult Index(string option, string search, int? page, string sort)
        {
            int pageNumber = page ?? 1;
            int pageSize = 5;

            //if the sort parameter is null or empty then we are initializing the value as descending Id  
            ViewBag.SortById = string.IsNullOrEmpty(sort) ? "descending Id" : "";
            //if the sort value is Email then we are initializing the value as descending Email 
            ViewBag.SortByEmail = sort == "Email" ? "descending Email" : "Email";
            //if the sort value is FirstName then we are initializing the value as descending FirstName 
            ViewBag.SortByFirstName = sort == "FirstName" ? "descending FirstName" : "FirstName";
            //if the sort value is LastName then we are initializing the value as descending LastName  
            ViewBag.SortByLastName = sort == "LastName" ? "descending LastName" : "LastName";
            //if the sort value is UserName then we are initializing the value as descending UserName  
            ViewBag.SortByUserName = sort == "UserName" ? "descending UserName" : "UserName";
            //here we are converting the db.user to AsQueryable so that we can invoke all the extension methods on variable records.  
            var records = db.Users.AsQueryable();

            //if a user choose the radio button option as FirstName  
            if (option == "FirstName")
            {
                //Index action method will return a view with a user records based on what a user specify the value in textbox  
                records = db.Users.Where(x => x.FirstName.StartsWith(search) || search == null);
            }
            else
            {
                records = db.Users.Where(x => x.LastName.StartsWith(search) || search == null);
                //return View(db.Users.Where(x => x.LastName == search || search == null).ToList());
            }

            switch (sort)
            {
                case "descending Id":
                    records = records.OrderByDescending(x => x.Id);
                    break;

                case "descending Email":
                    records = records.OrderByDescending(x => x.Email);
                    break;

                case "Email":
                    records = records.OrderBy(x => x.Email);
                    break;

                case "descending FirstName":
                    records = records.OrderByDescending(x => x.FirstName);
                    break;

                case "FirstName":
                    records = records.OrderBy(x => x.FirstName);
                    break;

                case "descending LastName":
                    records = records.OrderByDescending(x => x.LastName);
                    break;

                case "LastName":
                    records = records.OrderBy(x => x.LastName);
                    break;

                case "descending UserName":
                    records = records.OrderByDescending(x => x.UserName);
                    break;

                case "UserName":
                    records = records.OrderBy(x => x.UserName);
                    break;

                default:
                    records = records.OrderBy(x => x.Id);
                    break;
            }
            return View(records.ToPagedList(pageNumber, pageSize));
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
