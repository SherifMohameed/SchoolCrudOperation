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
    public class DepartmentController : Controller
    {
        private Model1 db = new Model1();

        // GET: Department
        public ActionResult Index()
        {
            return View(db.Departments.ToList());
        }

        // GET: Department/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // GET: Department/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Department/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "dept_id,dept_name")] Department department)
        {
            if (ModelState.IsValid)
            {
                db.Departments.Add(department);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(department);
        }


        public ActionResult AddCrsToDept(int id)
        {
            var depCrs = db.DeptCrs.Where(a => a.dept_id == id).Select(s => s.Course).ToList();
            var allCrs = db.Courses.ToList<Course>();
            var nds = allCrs.Except(depCrs).ToList<Course>();
            return View(nds);
        }
        [HttpPost]
        public ActionResult AddCrsToDept(int id,int[] course)
        {
            var std = db.Departments.Find(id).students.ToList();
            if (course.Length==0)
            {
                return RedirectToAction("Index");
            }

            foreach (var item in course)
            {
                db.DeptCrs.Add(new DeptCrs() { dept_id = id, crs_id = item });
            }
            db.SaveChanges();

            //var crs = db.Departments.Find(id).DeptCrs.Select(c => c.Course).ToList();


            if (std.Count>0)
            {
                foreach (var item in std)
                {
                    foreach (var item2 in course)
                    {
                        db.StuCrs.Add(new StuCrs { crs_id = item2, st_id = item.id });
                    }
                }
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult RemCrsToDept(int id)
        {
            var depCrs = db.DeptCrs.Where(a => a.dept_id == id).Select(s => s.Course).ToList();
            return View(depCrs);
        }
        [HttpPost]
        public ActionResult RemCrsToDept(int id, int[] course)
        {
            var std = db.Departments.Find(id).students.ToList();
            if (course.Length == 0)
            {
                return RedirectToAction("Index");
            }

            if (std.Count > 0)
            {
                foreach (var item in std)
                {

                    foreach (var item2 in course)
                    {
                        var stdCrs = db.StuCrs.FirstOrDefault(a => a.st_id == item.id&&a.crs_id==item2);
                        if (stdCrs != null)
                        {
                            db.StuCrs.Remove( stdCrs);
                        }
                    }
                }
            }
            db.SaveChanges();

            foreach (var item in course)
            {
                var stdCrs = db.DeptCrs.FirstOrDefault(a => a.crs_id == item);
                db.DeptCrs.Remove(stdCrs);
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ShowCrsINDept(int id)
        {
            var depCrs = db.DeptCrs.Where(a => a.dept_id == id).Select(s => s.Course).ToList();
            var std = db.Departments.Find(id).students.ToList();
            SelectList crs = new SelectList(depCrs, "crs_id", "crs_name");
            ViewBag.crs = crs;
            
            return View(std);
        }
        [HttpPost]
        public ActionResult ShowCrsINDept(int id, int Course,Dictionary<string,int> std)
        {
            //var stud = db.Departments.Find(id).students.ToList();

            if (std.Count > 0)
            {
                foreach (KeyValuePair<string,int> item in std)
                {
                      var stdCrs = db.StuCrs.FirstOrDefault(a => a.st_id.ToString() == item.Key && a.crs_id == Course);
                    if (stdCrs != null)
                       {
                        stdCrs.garde = item.Value;
                       }
                    else
                    {
                        stdCrs = new StuCrs { st_id = int.Parse(item.Key), crs_id = Course, garde = item.Value };
                        db.StuCrs.Add(stdCrs);

                    }


                }
            }

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Department/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: Department/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "dept_id,dept_name")] Department department)
        {
            if (ModelState.IsValid)
            {
                db.Entry(department).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(department);
        }

        // GET: Department/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: Department/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Department department = db.Departments.Find(id);
            db.Departments.Remove(department);
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
