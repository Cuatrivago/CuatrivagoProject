using CuatrivagoProject.Context;
using CuatrivagoProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace CuatrivagoProject.Controllers
{
    public class DetailRoomController : Controller
    {
        private string conn = WebConfigurationManager.ConnectionStrings["connectionDB"].ToString();
        private RoomContext roomContext = new RoomContext();
        private HotelContext hotelContext = new HotelContext();
        // GET: DetailRoom
        public ActionResult Index()
        {
            return View();
        }

        // GET: DetailRoom/Details/5
        public ActionResult Details(int idReservation, int back, string datein, string dateout)
        {

            ViewBag.id = idReservation;
            ViewBag.back = back;
            ViewBag.datein = datein;
            ViewBag.dateout = dateout;

            ModelForRoomList roomList = new ModelForRoomList();
            roomList.hotel = hotelContext.getInformationHotel(conn);
            roomList.roomList = roomContext.getRoomsByType(conn, 1);
            roomList.back = 0;
            roomList.room = roomContext.getRoom(conn, idReservation);
                //dfdfdfd
                //fsdfsdfsdf
            return View("Index", roomList);
        }

        // GET: DetailRoom/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DetailRoom/Create
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

        // GET: DetailRoom/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DetailRoom/Edit/5
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

        // GET: DetailRoom/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DetailRoom/Delete/5
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
