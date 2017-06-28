using CuatrivagoProjectAdmin.Context;
using CuatrivagoProjectAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace CuatrivagoProjectAdmin.Controllers
{
    public class OfferController : Controller
    {
        private string conn = WebConfigurationManager.ConnectionStrings["connectionDB"].ToString();
        OfferContext offerContext = new OfferContext();
        RoomContext room = new RoomContext();
        List<Offer> offer = new List<Offer>();

        public ActionResult Index()
        {
            //Estos If se necesitan en todos los controllers del modulo administrativo

            //Este pregunta si existe una cookie con el id del admin, esta se crea SI este inicia sesion con naturalidad.
            //Tambien pregunta si el adminId no es nulo, esto para evitar un caso raro donde la galleta exista pero no el id.
            if (Request.Cookies["Admin"] != null && Request.Cookies["Admin"]["adminId"] != null)
            {
                int access = int.Parse(Request.Cookies["Admin"]["adminId"]);

                //Si el id en la galleta es mayor a 0 es un administrador normal, pero si es menor a 0 es un
                //  codigo que se asigna cuando no pudo autenticar
                if (access > 0)
                {
                    //Aquí va su código normal, lo demás es control de seguridad.

                    return View(room.getAllRoomType(conn));
                }
                else
                {
                    //Este else retorna al index de logueo con un -2, el -2 le dice al index que este usuario intentó
                    //  entrar de forma no autorizada al modulo y se le presentará un mensaje sobre eso.
                    Request.Cookies["Admin"]["adminId"] = "-2";
                    return RedirectToAction("Index", "Login"); //Cambio de redirect, este es mejor
                }
            }
            else
            {
                //Este else es en caso de que la cookie no exista, se crea y se le da el codigo -2 para devolverlo a
                //  loguearse al index.
                HttpCookie galleta = new HttpCookie("Admin");
                galleta["adminId"] = "-2";
                galleta.Expires = DateTime.Now.AddMinutes(1);

                Response.Cookies.Add(galleta);
                return RedirectToAction("Index", "Login"); //Cambio de redirect, este es mejor
            }

        }

        //POST: RangeDateRoom/viewsRooms
        [HttpPost]
        public ActionResult offerView(FormCollection form)
        {
            DateTime dateIn = DateTime.Parse(Request.Form["dateIn"]);
            DateTime dateOut = DateTime.Parse(Request.Form["dateOut"]);
            int discount = int.Parse(Request.Form["discount"]);
            int type = int.Parse(Request.Form["type"]);
            offer = offerContext.createOffer(conn, dateIn, dateOut, discount, type);
            return View(offer);
        }

        [HttpPost]
        public ActionResult deleteOfferView()
        {
            offer = offerContext.getOffers(conn);
            return View(offer);
        }

        [HttpPost]
        public ActionResult deleteOffer(int id)
        {
            offerContext.deleteOffer(conn, id);
            return RedirectToAction("deleteOfferView");
        }
    }
}