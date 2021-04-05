using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SecurityInMVC.Models;
using SecurityInMVC.Controllers;
using System.Web.Security;//for web authentication

namespace SecurityInMVC.Controllers
{
    public class DefaultController : Controller
    {
        LTIMVCEntities db = new LTIMVCEntities();
        // GET: Default
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index() //no auth required
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register() //no auth required
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous] // skip authorization, begane log
        public ActionResult Register(Employee emp)
        {
            db.Employees.Add(emp);
            var res = db.SaveChanges();
            if (res > 0)
                ModelState.AddModelError("","Employee Registered");
            return View();
        }
        [HttpGet]
        [Authorize] //For Authentication lool
        public ActionResult SelectEmployees()
        {
            var data = db.Employees.ToList();
            return View(data);
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(string name, string pwd)
        {
            name = Request.Form["name"];
            pwd = Request.Form["pwd"];
            var query = db.Employees.Where(x => x.EmpName == name && x.password == pwd);
            if (query.Count() == 0)
                ModelState.AddModelError("", "Invalid Credentials");
            else
                FormsAuthentication.RedirectFromLoginPage(name, false);
            return View();
        }
    }
}