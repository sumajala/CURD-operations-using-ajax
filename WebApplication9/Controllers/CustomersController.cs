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
    public class CustomersController : Controller
    {
        private JqueryEntities db = new JqueryEntities();

        public ActionResult Index()
        {
            var model = db.Customers.ToList();
            return View(model);
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerID,Name,Age, Address")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        [HttpPost]
        public ActionResult CreateAjax([Bind(Include = "CustomerID, Name, Age, Address")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();

                return Json(new
                {
                    status = 200,
                    msg = "Successfully create a new customer! "
                });
            }

            return Json(new
            {
                status = 400,
                msg = "Failed to create a new customer! "
            });
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerID,Name, Age, Address")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/EditModal/5
        public ActionResult EditModal(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);

            var s = Json(customer/*, JsonRequestBehavior.AllowGet*/);

            if (customer == null)
            {
                return HttpNotFound();
            }

            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult EditModal([Bind(Include = "CustomerID,Name, Age, Address")] Customer customer)
        {
            db.Configuration.ProxyCreationEnabled = false;

            var c = db.Customers.Find(customer.CustomerID);
            var s = Json(c, JsonRequestBehavior.AllowGet);

            if (c != null)
            {
                c.Age = customer.Age;
                c.Name = customer.Name;
                c.Address = customer.Address;

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

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
           // db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteAjax(int id)
        {
            Customer customer = db.Customers.Find(id);

            if (customer != null)
            {
                try
                {
                    db.Customers.Remove(customer);
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
