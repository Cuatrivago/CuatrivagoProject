using System;
using CuatrivagoProjectAdmin.Models;
using CuatrivagoProjectAdmin.Context;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Configuration;

namespace CuatrivagoProjectAdmin.Controllers
{
    public class LoginController : Controller
    {
        private string conn = WebConfigurationManager.ConnectionStrings["connectionDB"].ToString();
        private AdminContext data = new AdminContext();

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        // GET: Login/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Login/Create
        public ActionResult Create()
        {
            return View();
        }

        //POST: Login/LogIn
        [HttpPost]
        public ActionResult LogIn(FormCollection collection)
        {
            string mail = collection["txtUser"];
            string pass = collection["txtPassword"];

            Admin local = new Admin();
            local.email = mail;
            local.password = pass;

            local = data.logAdmin(conn, local);

            //Independientemente de si existe el admin se asigna el valor, pues el -1 se utilizara para mostrar un mensaje
            //  en caso de que el admin no exista.
            HttpCookie galleta = new HttpCookie("Admin");
            galleta["adminId"] = local.idAdmin + "";

            if (local.idAdmin != -1)
            {
                galleta.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Add(galleta);
                return View("~/Views/AdminProfile/Index.cshtml");
            } else
            {
                galleta.Expires = DateTime.Now.AddMinutes(1);
                Response.Cookies.Add(galleta);
                return RedirectToAction("Index");
            }
        }
        //    Login/LogOut
        public ActionResult LogOut()
        {
            HttpCookie galleta = new HttpCookie("Admin");
            galleta["adminId"] = null;

            galleta.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(galleta);

            return RedirectToAction("Index");
        }

        public ActionResult NotFound()
        {
            return View();
        }

        // POST: Login/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Login/Edit/5
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

        // GET: Login/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Login/Delete/5
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
