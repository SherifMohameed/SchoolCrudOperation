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
    public class StudentController : Controller
    {
        private Model1 db = new Model1();

        // GET: Student
        public ActionResult Index()
        {
            var students = db.Students.Include(s => s.Department);
            return View(students.ToList());
        }

        // GET: Student/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            ViewBag.dept_id = new SelectList(db.Departments, "dept_id", "dept_name");
            return View();
        }

        // POST: Student/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,age,email,password,image,dept_id")] Student student, HttpPostedFileBase img)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                string[] arr = img.FileName.Split('.');
                string imgName = student.id + "." + arr[arr.Length - 1];
                img.SaveAs(Server.MapPath("~/Images/") + imgName);
                student.image = imgName;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.dept_id = new SelectList(db.Departments, "dept_id", "dept_name", student.dept_id);
            return View(student);
        }

        // GET: Student/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.dept_id = new SelectList(db.Departments, "dept_id", "dept_name", student.dept_id);
            return View(student);
        }


        // POST: Student/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,age,email,password,image,dept_id")] Student student, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (student.image != null)
                {
                    string[] arr = image.FileName.Split('.');
                    string imgName = student.id + "." + arr[arr.Length - 1];
                    image.SaveAs(Server.MapPath("~/Images/") + imgName);
                    student.image = imgName;
                    db.Entry(student).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    student.image = db.Students.Find(student.id).image;
                    db.Students.Find(student.id).name = student.name;
                    db.Students.Find(student.id).age = student.age;
                    db.Students.Find(student.id).dept_id = student.dept_id;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.dept_id = new SelectList(db.Departments, "dept_id", "dept_name", student.dept_id);
            return View(student);
        }

        // GET: Student/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
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
