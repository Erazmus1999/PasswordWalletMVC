using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using PasswordWalletMVC.Models;
using System.Linq;
using System;

namespace PasswordWalletMVC.Controllers
{
    public class AccountController : Controller
    {

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
                ViewBag.Message = account.FirstName + " " + account.LastName + "Successfully registered.";
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
                    
                    HttpContext.Session.SetString("UserId"  , usr.UserId.ToString());
                    HttpContext.Session.SetString("UserName", usr.UserId.ToString());

                    return RedirectToAction("LoggedIn");
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
            if (HttpContext.Session.GetString("UserId") != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }


        public IActionResult Displayed_Passwords()
        {
            using (OurDbContext db = new OurDbContext())
            {
                return View(db.userAccount.ToList());
            }
          //zxzxzxz
        }
    }
}
