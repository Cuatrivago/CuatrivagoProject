﻿using CuatrivagoProject.Context;
using CuatrivagoProject.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace CuatrivagoProject.Controllers
{
    public class ReservationController : Controller
    {
        // GET: Reservation
        private string conn = WebConfigurationManager.ConnectionStrings["connectionDB"].ToString();
        public ActionResult CreateReservation(int idReservation, int back, string datein, string dateout)
        {
            ViewBag.id = idReservation;
            ViewBag.back = back;
            ViewBag.datein = datein;
            ViewBag.dateout = dateout;
            return View();
        }
        [HttpPost]
        public JsonResult AjaxMethod(string email)
        {
            ClientContext cc = new ClientContext();
            int result = cc.verifyClient(conn, email);
            Client client = new Client();
            client.id = -1;
            if (result == 1)
            {
                client = cc.returnClient(conn, email);
            }
            return Json(client);
        }
        public ActionResult Insert()
        {
            string name = Request.Form["name"].ToString();
            string lastName = Request.Form["lastName"].ToString();
            string mail = Request.Form["mail"].ToString();
            int cardNumber = Int32.Parse(Request.Form["cardNumber"].ToString());
            int phone = Int32.Parse(Request.Form["phone"].ToString());
            string dateIn = Request.Form["arrival"].ToString();
            string dateOut = Request.Form["departure"].ToString();
            int idRoom = Int32.Parse(Request.Form["idRoom"].ToString());
            Client client = new Client(0,name,lastName,mail,cardNumber,phone,0);
            int clientId = 0;
            RoomContext rc = new RoomContext();
            int roomAvaible = rc.verifyRoomAvailable(conn, dateIn, dateOut, idRoom);
            string msgs = "";
            if (roomAvaible == 1)
            {
                ClientContext cc = new ClientContext();
                int clientExists = cc.returnClientId(conn, client.email);

                if (clientExists == -1)
                {
                    clientId = cc.insertClient(conn, client);
                }
                else
                {
                    clientId = clientExists;
                }

                ReservationContext rec = new ReservationContext();
                string result = rec.insertReservation(conn, dateIn, dateOut, clientId, idRoom);
                if (result == "n")
                {
                    msgs = "NO RESARVADA";
                }
                else
                {
                    msgs = "RESARVADA";
                }
            }
            else
            {
                msgs = "NO RESARVADA";
            }
            return RedirectToAction("msg", "Index", new { viewmsg = msgs });
        }
    }
}