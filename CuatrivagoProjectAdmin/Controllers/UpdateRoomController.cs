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
    public class UpdateRoomController : Controller
    {

        private string conn = WebConfigurationManager.ConnectionStrings["connectionDB"].ToString();
        RoomContext room = new RoomContext();
        UpdateRoomContext roomUpdate = new UpdateRoomContext();
        // GET: UpdateRoom
        public ActionResult Index()
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


                    return View(room.getAllRoomType(conn));
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
        //POST: RangeDateRoom/viewsRooms
        [HttpGet]
        public ActionResult viewsRoomsByType(int type)
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


                    return View(roomUpdate.getAllRommByType(conn, type));
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

        // POST: RoomImage/Create

        public ActionResult UpdateOneRoom()
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
                    string name = Request.Form["description"].ToString();
                    int capacity = int.Parse(Request.Form["capacity"].ToString());
                    int price = int.Parse(Request.Form["price"].ToString());
                    int idRoom = int.Parse(Request.Form["idRoom"].ToString());
                    int idTypeRoom = int.Parse(Request.Form["idTypeRoom"].ToString());
                    Room room = new Room();
                    room.description_ = name;
                    room.capacity = capacity;
                    room.price = price;
                    room.idRoom = idRoom;
                    room.roomType = idTypeRoom;
                    if (name.Length > 1 && capacity > 0 && price > 0)
                    {
                        int result = roomUpdate.updateRoomByType(conn, room);
                        if (result != -1)
                        {
                            return View(room);
                        }
                        else
                        {
                            HttpCookie cookie = new HttpCookie("error");
                            cookie["errorUpdate"] = "error";
                            cookie.Expires = DateTime.Now.AddSeconds(4);
                            Response.Cookies.Add(cookie);
                            return RedirectToAction("viewsRoomsByType", "UpdateRoom", new { type = idTypeRoom });
                        }
                    }
                    else {
                        HttpCookie cookie = new HttpCookie("errorData");
                        cookie["errorData"] = "error";
                        cookie.Expires = DateTime.Now.AddSeconds(4);
                        Response.Cookies.Add(cookie);
                        return RedirectToAction("viewsRoomsByType", "UpdateRoom", new { type = idTypeRoom });
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



        // GET: UpdateRoom/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UpdateRoom/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UpdateRoom/Create
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

        // GET: UpdateRoom/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UpdateRoom/Edit/5
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

        // GET: UpdateRoom/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UpdateRoom/Delete/5
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
