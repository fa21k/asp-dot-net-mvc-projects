using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using payrellunrwa.Models;

namespace payrellunrwa.Controllers
{
    public class SalariesController : Controller
    {
        private PayrollEntities db = new PayrollEntities();

        // GET: Salaries
        public ActionResult Index(Salary s)
        {

            //return View(db.Salary.ToList());
            //if (s.Basic_Salary > 5000)
            //       {
            //            s.Bonus = s.Basic_Salary * 10 / 100;
            //        }
            //       else if(s.Basic_Salary<5000)
            //        {
            //            s.Discount = s.Basic_Salary * 15 / 100;
            //            s.Overtime = s.Basic_Salary*5 / 100;
            //        }
            //       else
            //        {
            //            s.Overtime = 0;
            //        }
            //        s.Basic_Salary = Convert.ToInt32(s.Basic_Salary - s.Overtime);
            return View(db.Salary.ToList());
        }

        // GET: Salaries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salary salary = db.Salary.Find(id);
            if (salary == null)
            {
                return HttpNotFound();
            }
            return View(salary);
        }

        // GET: Salaries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Salaries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SalaryID,EmpId,Month,Year,Basic_Salary,Discount,Bonus,Overtime,Penalties,Penatlies_days")] Salary salary)
        {
            if (ModelState.IsValid)
            {
                if (salary.Basic_Salary > 5000)
                {
                    salary.Bonus = salary.Basic_Salary * 10 / 100;
                }
                else if (salary.Basic_Salary < 5000)
                {
                    salary.Discount = salary.Basic_Salary * 15 / 100;
                    salary.Overtime = salary.Basic_Salary * 5 / 100;
                }
                else
                {
                    salary.Overtime = 0;
                }
                salary.Basic_Salary = Convert.ToInt32(salary.Basic_Salary - salary.Overtime);
                //netsalary = salary.Basic_Salary + salary.Bonus + salary.Overtime - salary.Discount - salary.Penalties;
                db.Salary.Add(salary);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(salary);
        }

        // GET: Salaries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salary salary = db.Salary.Find(id);
            if (salary == null)
            {
                return HttpNotFound();
            }
            return View(salary);
        }

        // POST: Salaries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SalaryID,EmpId,Month,Year,Basic_Salary,Discount,Bonus,Overtime,Penalties,Penatlies_days")] Salary salary)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salary).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(salary);
        }

        // GET: Salaries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salary salary = db.Salary.Find(id);
            if (salary == null)
            {
                return HttpNotFound();
            }
            return View(salary);
        }

        // POST: Salaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Salary salary = db.Salary.Find(id);
            db.Salary.Remove(salary);
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
