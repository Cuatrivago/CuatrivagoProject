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
            //Con esta condicion llamamos al sp de activar ofertas 1 vez al dia, si ya fue llamado hace nada.
            if (Request.Cookies["verifyOffer"] == null) {
                HttpCookie galleta = new HttpCookie("verifyOffer");
                OfferContext data = new OfferContext();

                data.callForCheckOffer(this.conn);

                galleta["checked"] = "y";

                galleta.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Add(galleta);
            }

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
                return RedirectToAction("Index", "AdminProfile");
            } else
            {
                galleta.Expires = DateTime.Now.AddMinutes(1);
                Response.Cookies.Add(galleta);
                return RedirectToAction("Index");
            }
        }

        public ActionResult LogOut()
        {
            HttpCookie galleta = new HttpCookie("Admin");
            galleta["adminId"] = null;
            galleta["name"] = null;
            galleta.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(galleta);
            return RedirectToAction("Index");
        }
    }
}
