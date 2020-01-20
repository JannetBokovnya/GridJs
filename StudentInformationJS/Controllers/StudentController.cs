using StudentInformationJS.Entities;
using StudentInformationJS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentInformationJS.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetStudents(string sidx, string sort, int page, int rows, bool _search, string searchField, string searchOper, string searchString)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            sort = (sort == null) ? "" : sort;
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;

            var StudentList = db.Students.Select(
                    t => new
                    {
                        t.ID,
                        t.Name,
                        t.FatherName,
                        t.Gender,
                        t.ClassName,
                        t.DateOfAdmission
                    });
            if (_search)
            {
                switch (searchField)
                {
                    case "Name":
                        StudentList = StudentList.Where(t => t.Name.Contains(searchString));
                        break;
                    case "FatherName":
                        StudentList = StudentList.Where(t => t.FatherName.Contains(searchString));
                        break;
                    case "Gender":
                        StudentList = StudentList.Where(t => t.Gender.Contains(searchString));
                        break;
                    case "ClassName":
                        StudentList = StudentList.Where(t => t.ClassName.Contains(searchString));
                        break;
                }
            }
            int totalRecords = StudentList.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);
            if (sort.ToUpper() == "DESC")
            {
                StudentList = StudentList.OrderByDescending(t => t.Name);
                StudentList = StudentList.Skip(pageIndex * pageSize).Take(pageSize);
            }
            else
            {
                StudentList = StudentList.OrderBy(t => t.Name);
                StudentList = StudentList.Skip(pageIndex * pageSize).Take(pageSize);
                //test
            }
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = StudentList
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string Create([Bind(Exclude ="Id")] StudentMaster Model)
        {
            using (var db = new ApplicationDbContext())
            {
                string msg;
                try
                {
                    if (ModelState.IsValid)
                    {
                        Model.ID = Guid.NewGuid().ToString();
                        db.Students.Add(Model);
                        db.SaveChanges();
                        msg = "Saved Successfully";
                    }
                    else
                    {
                        msg = "Validation data not successfully";
                    }
                }
                catch (Exception ex)
                {
                    msg = "Error occured:" + ex.Message;
                }
                return msg;

            }
        }

        public string Edit(StudentMaster Model)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            string msg;
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(Model).State = EntityState.Modified;
                    db.SaveChanges();
                    msg = "Saved Successfully";
                }
                else
                {
                    msg = "Validation data not successfully";
                }
            }
            catch (Exception ex)
            {
                msg = "Error occured:" + ex.Message;
            }
            return msg;
        }

        public string Delete(string Id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            StudentMaster student = db.Students.Find(Id);
            db.Students.Remove(student);
            db.SaveChanges();
            return "Deleted successfully";
        }
    }
}