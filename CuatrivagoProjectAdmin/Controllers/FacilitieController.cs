﻿using CuatrivagoProjectAdmin.Context;
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
    public class FacilitieController : Controller
    {
        private string conn = WebConfigurationManager.ConnectionStrings["connectionDB"].ToString();
        private string connUCR = WebConfigurationManager.ConnectionStrings["connectionUCR"].ToString();
        ImageContext image = new ImageContext();
        FacilitieContext facilitieC = new FacilitieContext();

        // GET: Facilitie
        public ActionResult Index()
        {
            return View();
        }

        // GET: Facilitie/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateFacilitie()
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
                    string name = Request.Form["txtNameFacilitie"].ToString();
                    string description = Request.Form["txtDescription"].ToString();
                    Facilitie facilitie = new Facilitie();
                    facilitie.Name = name;
                    facilitie.Description = description;
                    if (name.Length > 1 && description.Length > 1)
                    {
                        int result = facilitieC.createFacilite(conn,facilitie);
                        if (result != -1)
                        {

                            HttpCookie cookie = new HttpCookie("idFacilitie");
                            cookie["idNewFacilitie"] = result.ToString();
                            cookie.Expires = DateTime.Now.AddSeconds(10);
                            Response.Cookies.Add(cookie);
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            HttpCookie cookie = new HttpCookie("error");
                            cookie["errorCreate"] = "error";
                            cookie.Expires = DateTime.Now.AddSeconds(4);
                            Response.Cookies.Add(cookie);
                            return RedirectToAction("Index");
                        }
                    }
                    else
                    {
                        HttpCookie cookie = new HttpCookie("errorData");
                        cookie["errorData"] = "error";
                        cookie.Expires = DateTime.Now.AddSeconds(4);
                        Response.Cookies.Add(cookie);
                        return RedirectToAction("Index");
                    }
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
            imageM.description = "Imagen Facilidad";
            imageM.path = thePictureDataAsString;

            int idImage = image.createImageByObject(connUCR, imageM);
            int result = 0;
            Boolean flag = false;
            if (idImage != -1)
            {
                result = image.asociateImageObject(connUCR, 'F', id, idImage);
            }
            else
            {
                flag = true;
            }
            if (result != -1)
            {
                HttpCookie cookie = new HttpCookie("success");
                cookie["successImage"] = "success";
                cookie.Expires = DateTime.Now.AddSeconds(10);
                Response.Cookies.Add(cookie);
                return RedirectToAction("Index");
            }
            else
            {
                flag = true;
            }

            if (flag)
            {
                HttpCookie cookie = new HttpCookie("error");
                cookie["errorImage"] = "error";
                cookie.Expires = DateTime.Now.AddSeconds(4);
                Response.Cookies.Add(cookie);
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");

        }

        public ActionResult showDeleteFacilities()
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
                    return View(facilitieC.getFacilities(conn));
                }
                else
                {
                    //Este else retorna al index de logueo con un -2, el -2 le dice al index que este usuario intentó
                    //  entrar de forma no autorizada al modulo y se le presentará un mensaje sobre eso.
                    Request.Cookies["Admin"]["adminId"] = "-2";
                    return RedirectToAction("Index", "Login");
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
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult deleteFacilities(int id)
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
                    facilitieC.deleteFacilitie(conn, id);
                    return RedirectToAction("showDeleteFacilities");
                }
                else
                {
                    //Este else retorna al index de logueo con un -2, el -2 le dice al index que este usuario intentó
                    //  entrar de forma no autorizada al modulo y se le presentará un mensaje sobre eso.
                    Request.Cookies["Admin"]["adminId"] = "-2";
                    return RedirectToAction("Index", "Login");
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
                return RedirectToAction("Index", "Login");
            }
        }




    }
}
