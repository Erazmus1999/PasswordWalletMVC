using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using PasswordWalletMVC.Models;
using System.Linq;
using System;
using System.Security.Principal;
using System.Collections.Generic;
using System.Diagnostics;

namespace PasswordWalletMVC.Controllers
{
    public class AccountController : Controller
    {
        public int global_var { get; set; }
        public IActionResult Index()
        {
           using(OurDbContext db = new OurDbContext())
            {
                return View(db.userAccount.ToList());
            }
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserAccount account)
        {
            if(ModelState.IsValid)
            {
                using(OurDbContext db = new OurDbContext())
                {
                    db.userAccount.Add(account);                   
                    db.SaveChanges();
                }
                ModelState.Clear();
                // We will be displaying hashed password there           
            }   
            return View();
        }

        //Login
        public IActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Login(UserAccount user)
        {
            using(OurDbContext db = new OurDbContext())
            {
                UserAccount usr = null;
                try
                {
                    usr = db.userAccount.Single(u => u.UserName == user.UserName && u.Password == user.Password);
                }
                catch(InvalidOperationException e)
                {
                    ModelState.AddModelError("", "UserName or Password is wrong.");
                }


                if (usr != null)
                {
                    HttpContext.Session.SetString("UserId", usr.UserId.ToString());
                    HttpContext.Session.SetString("UserName", usr.UserName.ToString());
                    ViewBag.TotalStudents = HttpContext.Session.GetString("UserId");
                    //global_var = HttpContext.Session.GetString("UserId");
                    return View("LoggedIn");
                }
                else
                {
                    ModelState.AddModelError("", "UserName or Password is wrong.");

                }
                return View();
            }
           
        }
        public IActionResult LoggedIn()
        {
            return View();
        }
        [HttpPost]
        public IActionResult LoggedIn(UserAccount User, Passwd password_)
        {         
                using (OurDbContext db = new OurDbContext())
                {
                    
                    db.passwds.Add(password_);
                    password_.UserNameId = Int32.Parse(HttpContext.Session.GetString("UserId"));
                    db.SaveChanges();
                }
                ModelState.Clear();
                // We will be displaying hashed password there                       
            return View();
        }


        public IActionResult Displayed_Passwords(UserAccount User, Passwd password_)
        {
            using (OurDbContext db = new OurDbContext())
            {
                
                var query = (from g in db.passwds
                            join u in db.userAccount on g.UserNameId equals u.UserId
                            select new { g, u, }).ToList();

               // var matchingRecords = db.passwds.Where(x => context.User.UserId == password_.UserNameId).ToList();
                return View(query);
            }
        }

        //get the employees, which in TodayEmployees Table, but not exist in the YesterdayEmployee

        /*
        var query4 = (from t in db.passwds
                              where (from y in db.userAccount select y.UserId).Contains(t.UserNameId)
                              select t).ToList();
        UserAccount usr2 = null;

        List<Passwd> numberList = new List<Passwd>();

        foreach (var element in db.passwds.ToList())
        {
            if(Int32.Parse(HttpContext.Session.GetString("UserId")) == password_.UserNameId)
            {
                numberList.Add(element);
                Console.WriteLine(element.PasswdName);
            }
        }

        return View(numberList);
        return View(db.passwds.ToList());

    }
      
    }
          */
    }
}
