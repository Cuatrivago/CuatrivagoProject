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
    public class HotelUpdateController : Controller
    {
        private string conn = WebConfigurationManager.ConnectionStrings["connectionDB"].ToString();
        private HotelContext hotelContext = new HotelContext();
        //private AdvertisementContext advertisementContext = new AdvertisementContext();
        //private FacilitieContext facilitieContext = new FacilitieContext();
        //private RoomTypeContext roomTypeContext = new RoomTypeContext();
        public ActionResult UpdateHotel()
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
    }
}