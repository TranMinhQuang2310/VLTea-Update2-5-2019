using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class VLTeaController : Controller
    {
        private CS4PEEntities db = new CS4PEEntities();

        // GET: /VLTea/
        public ActionResult Index()
        {
            var model = db.BubleTeas.ToList();
            return View(model);
        }

        // GET: /VLTea/Details/5
        public ActionResult Details(int? id)
        {
            var model = db.BubleTeas.Find(id);
            if(model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // GET: /VLTea/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /VLTea/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="id,Name,Price,Topping")] BubleTea model)
        {
            ValidateProduct(model);
            if (ModelState.IsValid)
            {
                model.Expire = DateTime.Today;
                db.BubleTeas.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        private void ValidateProduct(BubleTea model)
        {
            if(model.Price <= 0)
            {
                ModelState.AddModelError("Price", "Giá sản phẩm phải lớn hơn 0!");
            }
            if (String.IsNullOrEmpty(model.Name)){
                    ModelState.AddModelError("Name", "Phải có tên của sản phẩm!");
                }
            if (String.IsNullOrEmpty(model.Topping)){
                    ModelState.AddModelError("Topping", "Phải có tên của Topping!");
            }
            
        }

        // GET: /VLTea/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BubleTea bubletea = db.BubleTeas.Find(id);
            if (bubletea == null)
            {
                return HttpNotFound();
            }
            return View(bubletea);
        }

        // POST: /VLTea/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BubleTea model)
        {
            if (ModelState.IsValid)
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: /VLTea/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BubleTea bubletea = db.BubleTeas.Find(id);
            if (bubletea == null)
            {
                return HttpNotFound();
            }
            return View(bubletea);
        }

        // POST: /VLTea/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BubleTea bubletea = db.BubleTeas.Find(id);
            db.BubleTeas.Remove(bubletea);
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
