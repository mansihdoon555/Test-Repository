using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using _729solutions.Web.Models;

namespace _729solutions.Web.Controllers
{
    public class ProductEntitiesController : Controller
    {
        private StnSolutionsEntities db = new StnSolutionsEntities();

        // GET: ProductEntities
        public ActionResult Index()
        {
            return View(db.ProductEntities.ToList());
        }

        // GET: ProductEntities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductEntity productEntity = db.ProductEntities.Find(id);
            if (productEntity == null)
            {
                return HttpNotFound();
            }
            return View(productEntity);
        }

        // GET: ProductEntities/Create
        public ActionResult Create()
        {
            //return View();
            return View(new ProductEntity
            {

                NextRevisionDate = DateTime.Now

            });
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Price,Description,Active,Stock,NextRevisionDate")] ProductEntity productEntity)
        {
            if (ModelState.IsValid)
            {
                bool status = productEntity.Active.Value;
                if(status==true && productEntity.Name==null)
                {
                    ModelState.AddModelError("NameRequired", "Name cannot be blank");
                    return View(productEntity);
                }

                if (status == true && productEntity.Price == null)
                {
                    ModelState.AddModelError("PriceRequired", "Price is required");
                    return View(productEntity);
                }

                #region ID is Already Exists
                var isExist = IsIdExist(productEntity.ID);
                if (isExist)
                {
                    ModelState.AddModelError("IdExist", "Entered ID already exists");
                    return View(productEntity);
                }
                #endregion

                db.ProductEntities.Add(productEntity);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productEntity);
        }

        [NonAction]
        public bool IsIdExist(int ID)
        {
            using (StnSolutionsEntities dc = new StnSolutionsEntities())
            {
                var v = dc.ProductEntities.Where(a => a.ID == ID).FirstOrDefault();
                return v != null;
            }
        }

        // GET: ProductEntities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductEntity productEntity = db.ProductEntities.Find(id);
            if (productEntity == null)
            {
                return HttpNotFound();
            }
            return View(productEntity);
        }

        // POST: ProductEntities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Price,Description,Active,Stock,NextRevisionDate")] ProductEntity productEntity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productEntity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productEntity);
        }

        // GET: ProductEntities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductEntity productEntity = db.ProductEntities.Find(id);
            if (productEntity == null)
            {
                return HttpNotFound();
            }
            return View(productEntity);
        }

        // POST: ProductEntities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductEntity productEntity = db.ProductEntities.Find(id);
            db.ProductEntities.Remove(productEntity);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
