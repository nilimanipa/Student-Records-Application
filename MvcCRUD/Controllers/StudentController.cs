using MvcCRUD.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcCRUD.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetData()
        {
            using (DBModel db = new DBModel())
            {
                List<Student> studentList = db.Students.ToList<Student>();
                return Json(new { data = studentList }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult StoreOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new Student());
            else
            {
                using (DBModel db = new DBModel())
                {
                    return View(db.Students.Where(x => x.StudentID == id).FirstOrDefault<Student>());
                }
            }
        }

        [HttpPost]
        public ActionResult StoreOrEdit(Student studentob)
        {
            using (DBModel db = new DBModel())
            {
                if (studentob.StudentID == 0)
                {
                    db.Students.Add(studentob);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    db.Entry(studentob).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (DBModel db = new DBModel())
            {
                Student emp = db.Students.Where(x => x.StudentID == id).FirstOrDefault<Student>();
                db.Students.Remove(emp);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }


    }
}