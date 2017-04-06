using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CuatrivagoProject.Context;
using CuatrivagoProject.Models;
using System.Web.Configuration;

namespace CuatrivagoProject.Controllers
{
    public class IndexController : Controller
    {


        private string conn = WebConfigurationManager.ConnectionStrings["connectionDB"].ToString();

        private HotelContext hotelContext = new HotelContext();
        private AdvertisementContext advertisementContext = new AdvertisementContext();

        // GET: Index
        public ActionResult Index()
        {
            ModelsForIndex vm = new ModelsForIndex();

            vm.Hotel = hotelContext.getInformationHotel(conn);
            vm.advertisement = advertisementContext.getInformationAdvertisement(conn);

            return View(vm);
        }


    }
}
