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
    public class CreateRoomController : Controller
    {

        private string conn = WebConfigurationManager.ConnectionStrings["connectionDB"].ToString();
        RoomContext room = new RoomContext();
        // GET: CreateRoom
        public ActionResult Index()
        {
            return View(room.getAllRoomType(conn));
        }



        public ActionResult createRoom()
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
                    string name = Request.Form["txtNameRoom"].ToString();
                    int capacity = int.Parse(Request.Form["txtCapacity"].ToString());
                    int idTypeRoom = int.Parse(Request.Form["type"].ToString());
                    Room room2 = new Room();
                    room2.description_ = name;
                    room2.capacity = capacity;
                    room2.roomType = idTypeRoom;
                    if (name.Length > 1 && capacity > 0)
                    {
                        int result = room.createRoom(conn, room2);
                        if (result != -1)
                        {
                            return View(room2);
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

















        // GET: CreateRoom/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CreateRoom/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CreateRoom/Create
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

        // GET: CreateRoom/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CreateRoom/Edit/5
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

        // GET: CreateRoom/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CreateRoom/Delete/5
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
