using CuatrivagoProject.Context;
using CuatrivagoProject.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace CuatrivagoProject.Controllers
{
    public class RoomController : Controller
    {
        // GET: Room
        private string conn = WebConfigurationManager.ConnectionStrings["connectionDB"].ToString();
        private RoomContext roomContext = new RoomContext();
        private HotelContext hotelContext = new HotelContext();
        public ActionResult Index()
        {
            return View();
        }

        // GET: Room/Details/5
        [HttpGet]
        public ActionResult Details(int id, string colon, string dolar)
        {
            ModelForRoomList roomList = new ModelForRoomList();
            roomList.hotel = hotelContext.getInformationHotel(conn);
            Debug.WriteLine("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
            Debug.WriteLine(colon);
            roomList.roomList = roomContext.getRoomsByType(conn, id,colon, dolar);
            roomList.back = 0;


            return View("Index", roomList);
        }

        public ActionResult RoomTypeList()
        {
            string arrival = Request.Form["arrival"].ToString();
            string departure = Request.Form["departure"].ToString();
            int roomType = Int32.Parse(Request.Form["roomType"].ToString());
            ModelForRoomList roomList = new ModelForRoomList();
            roomList.roomList = roomContext.getRoomsAvailable(conn, arrival, departure, roomType);
            roomList.dateIn = arrival;
            roomList.hotel = hotelContext.getInformationHotel(conn);
            roomList.dateOut = departure;
            Debug.WriteLine(arrival + "  " + departure);
            roomList.back = 1;
            return View("Index", roomList);
        }


        // GET: Room/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Room/Create
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

        // GET: Room/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Room/Edit/5
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

        // GET: Room/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Room/Delete/5
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
