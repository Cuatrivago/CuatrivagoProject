using CuatrivagoProjectAdmin.Context;
using CuatrivagoProjectAdmin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace CuatrivagoProjectAdmin.Controllers
{
    public class ImagesHomeController : Controller
    {
        ImageContext image = new ImageContext();
        private string conn = WebConfigurationManager.ConnectionStrings["connectionUCR"].ToString();
        // GET: ImagesRoom
        public ActionResult Index()
        {
            return View();
        }

        // GET: ImagesRoom/Details/5
        [HttpGet]
        public ActionResult getImagesByObject(int id)
        {
            return View(image.getImagesByObject(conn, id, 'H'));
        }

        // GET: ImagesRoom/Create
        [HttpGet]
        public ActionResult Create(int idObject)
        {
            Hotel hotel = new Hotel();
            hotel.idHotel = idObject;
            return View(hotel);
        }

        [HttpPost]
        public ActionResult CreateImage()
        {

            int id = int.Parse(Request.Form["idObject"].ToString());
            HttpPostedFileBase imageRoom = Request.Files["file"];


            string contentType = imageRoom.ContentType.ToString();
            string theFileName = Path.GetFileName(imageRoom.FileName);
            byte[] thePictureAsBytes = new byte[imageRoom.ContentLength];
            using (BinaryReader theReader = new BinaryReader(imageRoom.InputStream))
            {
                thePictureAsBytes = theReader.ReadBytes(imageRoom.ContentLength);
            }

            string thePictureDataAsString = "data:" + contentType + ";base64," + Convert.ToBase64String(thePictureAsBytes);

            Image imageM = new Image();
            imageM.description = "Imagen Home";
            imageM.path = thePictureDataAsString;

            int idImage = image.createImageByObject(conn, imageM);
            int result = 0;
            Boolean flag = false;
            if (idImage != -1)
            {
                result = image.asociateImageObject(conn, 'H', id, idImage);
            }
            else
            {
                flag = true;
            }
            if (result != -1)
            {
                return RedirectToAction("Create", new { idObject = id });
            }
            else
            {
                flag = true;
            }

            if (flag)
            {
                HttpCookie cookie = new HttpCookie("error");
                cookie["errorUpdate"] = "error";
                cookie.Expires = DateTime.Now.AddSeconds(4);
                Response.Cookies.Add(cookie);
                return RedirectToAction("Create", new { idObject = id });
            }

            return RedirectToAction("Create", new { idObject = id });

        }



        [HttpGet]
        public ActionResult DeleteImage(int idImage, int idRoom)
        {
            int result = image.deleteImageById(conn, idImage);

            if (result == -1)
            {
                HttpCookie cookie = new HttpCookie("error");
                cookie["errorDelete"] = "error";
                cookie.Expires = DateTime.Now.AddSeconds(4);
                Response.Cookies.Add(cookie);
                return RedirectToAction("getImagesByObject", new { id = idRoom });
            }
            else
            {

                HttpCookie cookie = new HttpCookie("success");
                cookie["successDelete"] = "success";
                cookie.Expires = DateTime.Now.AddSeconds(4);
                Response.Cookies.Add(cookie);
                return RedirectToAction("getImagesByObject", new { id = idRoom });

            }

        }
    }
}
