using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CuatrivagoProjectAdmin.Models;
using CuatrivagoProjectAdmin.Context;
using System.Web.Configuration;

namespace CuatrivagoProjectAdmin.Controllers
{
    public class SignUpController : Controller
    {
        private string conn = WebConfigurationManager.ConnectionStrings["connectionDB"].ToString();
        private AdminContext data = new AdminContext();

        // GET: SignUp
        public ActionResult Index()
        {
            return View();
        }

        // GET: SignUp/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SignUp/Create
        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            Admin adm = new Admin();

            adm.name = form["first-name"];
            adm.lastName = form["last-name"];
            adm.email = form["Email"];
            adm.password = form["rePass"];

            HttpCookie galleta = new HttpCookie("operation");
            
            

            if (form["Pass"] != adm.password)
            {
                galleta["ans"] = "p";
            } else
            {
                galleta["ans"] = data.createAdmin(conn, adm) + "";
            }

            galleta.Expires = DateTime.Now.AddMinutes(2);
            Response.Cookies.Add(galleta);
            return RedirectToAction("Index");
        }

        // GET: SignUp/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SignUp/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: SignUp/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SignUp/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
