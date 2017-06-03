using CuatrivagoProjectAdmin.Context;
using CuatrivagoProjectAdmin.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace CuatrivagoProjectAdmin.Controllers
{
    public class HotelUpdateController : Controller
    {
        private string conn = WebConfigurationManager.ConnectionStrings["connectionDB"].ToString();
        private HotelContext hotelContext = new HotelContext();
        //private AdvertisementContext advertisementContext = new AdvertisementContext();
        //private FacilitieContext facilitieContext = new FacilitieContext();
        //private RoomTypeContext roomTypeContext = new RoomTypeContext();
        public ViewResult UpdateHotel()
        {


           Hotel vm = new Hotel();
            vm = hotelContext.getInformationHotel(conn);
            //vm.Hotel = hotelContext.getInformationHotel(conn);
            //vm.advertisement = advertisementContext.getInformationAdvertisement(conn);
            //vm.facilitie = facilitieContext.getAllFacilities(conn);
            //vm.roomType = roomTypeContext.getAllRoomType(conn);
            //vm.image = imageContext.getAllImage(conn);

            return View(vm);
            //return View();
        }
        //public RedirectResult UpdateHotelSave()
        //{
        //    return new RedirectResult("http://www.google.com");
        //}
        public ActionResult UpdateHotelSave()
        {
            Hotel newHotel = new Hotel();
            string name = "CUATRIVAGO";
            string address = Request.Form["address"].ToString();
            int phone = Int32.Parse(Request.Form["phone"].ToString());
            string email = Request.Form["email"].ToString();
            string email2= Request.Form["email2"].ToString();
            string about = Request.Form["about"].ToString();
            string home = Request.Form["home"].ToString();
            newHotel.idHotel= 1;
            newHotel.name = name;
            newHotel.address_ = address;
            newHotel.phone = phone +"";
            newHotel.email= email;
            newHotel.postMail = email2;
            newHotel.aboutUs = about;
            newHotel.homeInformation= home;
            hotelContext.updateHotel(newHotel, conn);

            Hotel vm = new Hotel();
            vm = hotelContext.getInformationHotel(conn);
            return View("UpdateHotel", vm);
        }
    }
}