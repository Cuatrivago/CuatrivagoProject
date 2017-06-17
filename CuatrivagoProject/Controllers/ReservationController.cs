using CuatrivagoProject.Context;
using CuatrivagoProject.Models;
using CuatrivagoProjectAdmin.Context;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
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
        [HttpPost]
        public JsonResult isAvaibleRoom(string arrival, string departure, int idRoom)
        {
            RoomContext rc = new RoomContext();
            int roomAvaible = rc.verifyRoomAvailable(conn, arrival, departure, idRoom);
            return Json(roomAvaible);
        }
        [HttpPost]
        public int Insert(string name, string lastName, string mail,
            int cardNumber, int phone, string dateIn, string dateOut, int idRoom, string subscription)
        {
            Debug.WriteLine("abc" + subscription + "1234");
            Client client = new Client(0, name, lastName, mail, cardNumber, phone, 0);
            int clientId = 0;
            RoomContext rc = new RoomContext();
            int roomAvaible = rc.verifyRoomAvailable(conn, dateIn, dateOut, idRoom);
            int msgs = 0;
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
            string result = rec.insertReservation(conn, dateIn, dateOut, clientId, idRoom, mail, subscription);
            if (result == "n")
            {
                msgs = -7; //no reservada
            }
            else
            {
                msgs = -11; //reservada 
            }
            return msgs;
        }
    }
}