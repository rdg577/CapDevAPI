using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapDevAPI.Models;

namespace CapDevAPI.Controllers
{
    public class APIController : Controller
    {
        CapDev db = new CapDev();

        public JsonResult SelectAllStudents()
        {
            // query all records from Students table
            // arrange by Lastname
            var list = db.Students.OrderBy(s => s.Lastname).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult InsertStudent(String Firstname, String Lastname)
        {
            int status = 0; // 0 - failed post of new student
            try
            {
                Student s = new Student();
                s.Firstname = Firstname;
                s.Lastname = Lastname;
                db.Students.Add(s);
                db.SaveChanges();

                status = 1; // 1 - successful post of new student
            }
            catch (Exception e)
            {
                throw;
            }

            return Json(status, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateStudent(int Id, String Firstname, String Lastname)
        {
            int status = 0; // 0 - failed update
            try
            {
                var s = db.Students.SingleOrDefault(a => a.Id == Id);
                if (s != null)
                {
                    s.Firstname = Firstname;
                    s.Lastname = Lastname;

                    db.SaveChanges();

                    status = 1; // 1 - successful
                }
            } catch (Exception )
            {
                throw;
            }

            return Json(status, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteStudent(int Id)
        {
            int status = 0; // 0 - failed update
            try
            {
                var s = db.Students.SingleOrDefault(a => a.Id == Id);
                if (s != null)
                {
                    db.Students.Remove(s);
                    db.SaveChanges();

                    status = 1; // 1 - successful
                }
            }
            catch (Exception)
            {
                throw;
            }

            return Json(status, JsonRequestBehavior.AllowGet);
        }

        public JsonResult TotalStudents()
        {
            var total = db.Students.Count();
            
            return Json(new { total }, JsonRequestBehavior.AllowGet);
        }
    }
}