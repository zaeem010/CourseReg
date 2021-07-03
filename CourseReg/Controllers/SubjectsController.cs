using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CourseReg.Models;
using CourseReg.ViewModel;

namespace CourseReg.Controllers
{
    public class SubjectsController : Controller
    {
         private ApplicationDbContext _context;

        public SubjectsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Subjects
        public ActionResult Index()
        {
            var list = _context.Database.SqlQuery<Subject_VMQ>("SELECT   Books.id, Books.Name, Chemesters.Name AS ChemsterName FROM Books INNER JOIN Chemesters ON Books.Chemsterid = Chemesters.id").ToList();
            return View(list);
        }
        public ActionResult Create(Books Books)
        {
            var list = _context.tbl_Chemester.ToList();
            var viewmodel = new Subject_VM
            {
                Books=Books,
                ScmesterList=list
            };
            return View(viewmodel);
        }   
        public ActionResult Save(Books Books)
        {
            string vardirection;
            if (Books.id == 0)
            {
                _context.tbl_Books.Add(Books);
                vardirection = "create";
            }
            else
            {
                var Booksdb = _context.tbl_Books.SingleOrDefault(c => c.id == Books.id);
                Booksdb.Name = Books.Name;
                Booksdb.Chemsterid = Books.Chemsterid;
               
                vardirection = "Index";
            }
            _context.SaveChanges();
            return RedirectToAction(vardirection);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            var listed = _context.tbl_Books.SingleOrDefault(c => c.id == id);
            var list = _context.tbl_Chemester.ToList();
            var viewmodel = new Subject_VM
            {
                Books = listed,
                ScmesterList = list
            };
          
            return View("create", viewmodel);
        }
        public ActionResult Delete(int id)
        {
            var Employee = _context.tbl_Books.SingleOrDefault(c => c.id == id);
            _context.tbl_Books.Remove(Employee);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}