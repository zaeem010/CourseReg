using CourseReg.Models;
using CourseReg.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseReg.Controllers
{
    public class CourseController : Controller
    {
        private ApplicationDbContext _context;

        public CourseController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Course
        public ActionResult Index()
        {
            var list = _context.Database.SqlQuery<Course_VMQ>("SELECT CourseRegistartions.id, CourseRegistartions.StdName, Chemesters.Name AS ChemesterName, Books.Name AS BookName FROM CourseRegistartions INNER JOIN Chemesters ON CourseRegistartions.Chemester = Chemesters.id INNER JOIN Books ON CourseRegistartions.Subjects = Books.id").ToList();
            return View(list);
        }
        public ActionResult Subjects(CourseRegistartion courseRegistartions)
        {
            var list = _context.tbl_Chemester.ToList();
            var viewmodel = new Subject_VM
            {
                CourseRegistartion=courseRegistartions,
                ScmesterList = list
            };
            return View(viewmodel);
        }
        [HttpPost]
        public ActionResult Create(CourseRegistartion courseRegistartion)
        {
            var BooksList = _context.Database.SqlQuery<Books>("SELECT * from Books WHERE Chemsterid= "+ courseRegistartion.Chemester +"").ToList();
            var list = _context.tbl_Chemester.ToList();
            var viewmodel = new Subject_VM
            {
                CourseRegistartion = courseRegistartion,
                ScmesterList = list,
                BookList=BooksList
            };
            return View(viewmodel);
        }
        public ActionResult Save(CourseRegistartion courseRegistartion ,string[] Bookid)
        {
            if (courseRegistartion.id == 0)
            {
                for (int i = 0; i < Bookid.Count(); i++)
                {
                    _context.Database.ExecuteSqlCommand("INSERT  INTO CourseRegistartions(StdName, Chemester, Subjects) VALUES ('"+ Session["StdName"] +"','"+ courseRegistartion.Chemester +"','"+ Bookid[i] +"')");
                }
            }
            
            return RedirectToAction("Subjects");
        }
        public ActionResult Delete(int id)
        {
            var Employee = _context.tbl_Course.SingleOrDefault(c => c.id == id);
            _context.tbl_Course.Remove(Employee);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}