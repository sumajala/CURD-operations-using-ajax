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
    public class ProductsController : Controller
    {
        private JqueryEntities db = new JqueryEntities();

        // GET: Products
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,Name,Price")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }
        [HttpPost]
        public ActionResult CreateAjax([Bind(Include = "ProductID, Name,Price")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();

                return Json(new
                {
                    status = 200,
                    msg = "Successfully create a new product! "
                });
            }

            return Json(new
            {
                status = 400,
                msg = "Failed to create a new product! "
            });
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        // GET: Customers/EditModal/5
        public ActionResult EditModal(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);

            var s = Json(product/*, JsonRequestBehavior.AllowGet*/);

            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult EditModal([Bind(Include = "ProductID,Name,Price")] Product product)
        {
            db.Configuration.ProxyCreationEnabled = false;

            var c = db.Products.Find(product.ProductID);
            var s = Json(c, JsonRequestBehavior.AllowGet);

            if (c != null)
            {
                
                c.Name = product.Name;
                c.Price = product.Price;

                db.SaveChanges();

                return Json(new
                {
                    status = 200,
                    msg = s
                });

            }
            else
            {
                return Json(new
                {
                    status = 400,
                    msg = "Something is wrong!"
                });
            }


        }
        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult DeleteAjax(int id)
        {
            Product product = db.Products.Find(id);

            if (product != null)
            {
                try
                {
                    db.Products.Remove(product);
                    db.SaveChanges();

                    return Json(new
                    {
                        status = 200,
                        msg = "Successfully deleted "
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
            else
            {
                return Json(new
                {
                    status = 400,
                    msg = "Failed to delete "
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
