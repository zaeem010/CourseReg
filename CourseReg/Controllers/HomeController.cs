using CourseReg.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CourseReg.Controllers
{
    public class SessionTimeoutAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
            if (HttpContext.Current.Session["UserID"] == null)
            {
                filterContext.Result = new RedirectResult("~/Home/Login");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(StdLogin stdLogin)
        {
            
            return View(stdLogin);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logins(StdLogin stdLogin)
        {
            if (ModelState.IsValid)
            {
                var obj = _context.tbl_stdLogin.Where(a => a.UserName.Equals(stdLogin.UserName) && a.Pass.Equals(stdLogin.Pass) ).FirstOrDefault();
                if (obj != null)
                {
                    Session["UserID"] = _context.Database.SqlQuery<int>("Select id From StdLogins where UserName = '" + stdLogin.UserName + "' AND Pass = '" + stdLogin.Pass + "' ").FirstOrDefault();
                    Session["UserName"] = obj.UserName.ToString();
                    Session["StdName"] = obj.StdName.ToString();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["Alert"] = "Invalid Login Details";
                    return RedirectToAction("Login");

                }
            }
            return View();
        }
        public ActionResult Indexre()
        {
            var list = _context.tbl_stdLogin.ToList();
            return View(list);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Register(StdLogin stdLogin)
        {
            return View(stdLogin);
        }
        [HttpPost]
        public ActionResult Registers(StdLogin stdLogin)
        {
            string vardirection;
            if (stdLogin.id == 0)
            {
                _context.tbl_stdLogin.Add(stdLogin);
                vardirection = "Register";
            }
            else
            {
                var stdLogindb = _context.tbl_stdLogin.SingleOrDefault(c => c.id == stdLogin.id);
                stdLogindb.StdName = stdLogin.StdName;
                stdLogindb.FatherName = stdLogin.FatherName;
                stdLogindb.Reg = stdLogin.Reg;
                stdLogindb.ColgJoin = stdLogin.ColgJoin;
                stdLogindb.Address = stdLogin.Address;
                stdLogindb.UserName = stdLogin.UserName;
                stdLogindb.Pass = stdLogin.Pass;
                
                vardirection = "Indexre";
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
            var listed = _context.tbl_stdLogin.SingleOrDefault(c => c.id == id);

            return View("Register", listed);
        }
        public ActionResult Delete(int id)
        {
            var Employee = _context.tbl_stdLogin.SingleOrDefault(c => c.id == id);
            _context.tbl_stdLogin.Remove(Employee);
            _context.SaveChanges();
            return RedirectToAction("Indexre");
        }
        public ActionResult LogOut()
        {
            Session["UserID"] = null;
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
           
            return RedirectToAction("Login");
        }
    }
}