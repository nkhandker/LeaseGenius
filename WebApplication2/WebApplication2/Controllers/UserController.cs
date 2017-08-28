using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Security.Cryptography;
using System.Text;


namespace WebApplication2.Controllers
{
    public class UserController : Controller
    {
        public string CalculateMD5Hash(string input)

        {

            // step 1, calculate MD5 hash from input

            MD5 md5 = System.Security.Cryptography.MD5.Create();

            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);

            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)

            {

                sb.Append(hash[i].ToString("X2"));

            }

            return sb.ToString();

        }




        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Models.User objUser)
        {
            if (ModelState.IsValid)
            {
                using (Models.leasingEntities db = new Models.leasingEntities())
                {
                    objUser.UserID = Guid.NewGuid();
                   
                        objUser.UserPassword = CalculateMD5Hash(objUser.UserPassword);

                    db.Users.Add(objUser);
                    db.SaveChanges();
                    /*
                    var obj = db.Users.Where(a => a.UserName.Equals(objUser.UserName) && a.UserPassword.Equals(objUser.UserPassword)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["UserID"] = obj.UserID.ToString();
                        Session["UserName"] = obj.UserName.ToString();
                        return RedirectToAction("UserDashBoard");
                    }
                    */
                }
            }
            ViewBag.Message = "Invalid credentials";
            return View(objUser);
        }




        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Models.User objUser)
        {
            if (ModelState.IsValid)
            {
                objUser.UserPassword = CalculateMD5Hash(objUser.UserPassword);
                using (Models.leasingEntities db = new Models.leasingEntities())
                {
                    var obj = db.Users.Where(a => a.UserName.Equals(objUser.UserName) && a.UserPassword.Equals(objUser.UserPassword)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["UserID"] = obj.UserID.ToString();
                        Session["UserName"] = obj.UserName.ToString();
                        return RedirectToAction("UserDashBoard");
                    }
                }
            }
            ViewBag.Message = "Invalid credentials";
            return View(objUser);
        }

        public ActionResult UserDashBoard()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
    }
}