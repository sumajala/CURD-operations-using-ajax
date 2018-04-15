using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication9.Models;

namespace WebApplication9.Controllers
{
    public class ProductSoldsController : Controller
    {
        private JqueryEntities db = new JqueryEntities();

        // GET: ProductSolds
        public ActionResult Index()
        {
            var productSolds = db.ProductSolds.Include(p => p.Customer).Include(p => p.Product).Include(p => p.Store);
            return View(productSolds.ToList());
        }

        // GET: ProductSolds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductSold productSold = db.ProductSolds.Find(id);
            if (productSold == null)
            {
                return HttpNotFound();
            }
            return View(productSold);
        }

        // GET: ProductSolds/Create
        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Name");
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name");
            ViewBag.StoreID = new SelectList(db.Stores, "StoreID", "Name");
            return View();
        }

        // POST: ProductSolds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ProductID,CustomerID,StoreID,DateSold")] ProductSold productSold)
        {
            if (ModelState.IsValid)
            {
                db.ProductSolds.Add(productSold);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Name", productSold.CustomerID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name", productSold.ProductID);
            ViewBag.StoreID = new SelectList(db.Stores, "StoreID", "Name", productSold.StoreID);
            return View(productSold);
        }
        [HttpPost]
        public ActionResult CreateAjax([Bind(Include = "ProductID, CustomerID, StoreID, DateSold, ID")] ProductSold productSold)
        {
            if (ModelState.IsValid)
            {
                db.ProductSolds.Add(productSold);
                db.SaveChanges();

                return Json(new
                {
                    status = 200,
                    msg = "Create a new sale Successfully!"
                });
            }

            return Json(new
            {
                status = 400,
                msg = "Create a new sale failed !"
            });
        }


        // GET: ProductSolds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductSold productSold = db.ProductSolds.Find(id);
            if (productSold == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Name", productSold.CustomerID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name", productSold.ProductID);
            ViewBag.StoreID = new SelectList(db.Stores, "StoreID", "Name", productSold.StoreID);
            return View(productSold);
        }

        // POST: ProductSolds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ProductID,CustomerID,StoreID,DateSold")] ProductSold productSold)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productSold).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Name", productSold.CustomerID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name", productSold.ProductID);
            ViewBag.StoreID = new SelectList(db.Stores, "StoreID", "Name", productSold.StoreID);
            return View(productSold);
        }
        [HttpPost]
        public ActionResult EditAjax([Bind(Include = "ProductID, CustomerID, StoreID, DateSold, ID")] ProductSold productSold)
        {
            if (ModelState.IsValid)
            {
                ProductSold p = db.ProductSolds.Find(productSold.ID);

                p.ProductID = productSold.ProductID;
                p.StoreID = productSold.StoreID;
                p.DateSold = productSold.DateSold;
                p.CustomerID = productSold.CustomerID;

                db.SaveChanges();

                return Json(new
                {
                    status = 200,
                    msg = "Edit saved !"
                });
            }

            return Json(new
            {
                status = 400,
                msg = "Edit saving failed!"
            });
        }

        // GET: ProductSolds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductSold productSold = db.ProductSolds.Find(id);
            if (productSold == null)
            {
                return HttpNotFound();
            }
            return View(productSold);
        }

        // POST: ProductSolds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductSold productSold = db.ProductSolds.Find(id);
            db.ProductSolds.Remove(productSold);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteAjax(int id)
        {
            ProductSold productSold = db.ProductSolds.Find(id);

            try
            {
                db.ProductSolds.Remove(productSold);
                db.SaveChanges();

                return Json(new
                {
                    status = 200,
                    msg = "Deleted the record!"
                });
            }
            catch (Exception e)
            {
                return Json(new
                {
                    status = 400,
                    msg = e.Message
                });
            }

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
