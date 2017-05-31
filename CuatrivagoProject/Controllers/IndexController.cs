using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CuatrivagoProject.Context;
using CuatrivagoProject.Models;
using System.Web.Configuration;
using System.Diagnostics;

namespace CuatrivagoProject.Controllers
{
    public class IndexController : Controller
    {
        private string conn = WebConfigurationManager.ConnectionStrings["connectionDB"].ToString();
        private HotelContext hotelContext = new HotelContext();
        private AdvertisementContext advertisementContext = new AdvertisementContext();
        private FacilitieContext facilitieContext = new FacilitieContext();
        private RoomTypeContext roomTypeContext = new RoomTypeContext();

        private ImageContext imageContext = new ImageContext();
        // GET: Index
        public ActionResult Index()
        {
            
            ModelsForIndex vm = new ModelsForIndex();
            Debug.WriteLine(conn);
            vm.Hotel = hotelContext.getInformationHotel(conn);
            vm.advertisement = advertisementContext.getInformationAdvertisement(conn);
            vm.facilitie = facilitieContext.getAllFacilities(conn);
            vm.roomType = roomTypeContext.getAllRoomType(conn);
            vm.image = imageContext.getAllImage(conn);

            return View(vm);
        }
        public ActionResult msg(string viewmsg)
        {

            ViewBag.result = viewmsg;
            ModelsForIndex vm = new ModelsForIndex();
            vm.Hotel = hotelContext.getInformationHotel(conn);
            vm.advertisement = advertisementContext.getInformationAdvertisement(conn);
            vm.facilitie = facilitieContext.getAllFacilities(conn);
            vm.roomType = roomTypeContext.getAllRoomType(conn);
            vm.image = imageContext.getAllImage(conn);


            return View("Index",vm);
        }

    }
}
