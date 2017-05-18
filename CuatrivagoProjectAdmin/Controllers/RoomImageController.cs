using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CuatrivagoProjectAdmin.Models;
using CuatrivagoProjectAdmin.Context;
using System.Web.Configuration;
using System.IO;
using System.Web.Hosting;
using System.Web.Services;

namespace CuatrivagoProjectAdmin.Controllers
{
    public class RoomImageController : Controller
    {
        private string conn = WebConfigurationManager.ConnectionStrings["connectionDB"].ToString();
        private RoomContext roomContext = new RoomContext();
        private ImageContext imgCon = new ImageContext();

        // GET: RoomImage
        public ActionResult Index()
        {
            //Esto dentro del codigo de seguridad
            ModelsForRoomImage list = new ModelsForRoomImage();

            list.roomList = roomContext.getRoomComboBox(conn);

            return View(list);
            //Eso dentro del codigo de seguridad
        }

        [HttpPost]
        public ActionResult getImage(string id)
        {
            List<Image> ima;

            ima = imgCon.getImage(this.conn, int.Parse(id));

            string server = "http://localhost:57673/";

            String htOb = "";

            foreach (Image temp in ima) {

                temp.path = temp.path.Substring(1);
                htOb = htOb + "<img src='" + server + temp.path + "' width='"+128+"' height='"+128+"' alt='Imagen' />";

                htOb = htOb + "<p> " + temp.description + " </p>";
            }

            HttpCookie galleta = new HttpCookie("r");
            galleta["r"] = id;
            galleta.Expires = DateTime.Now.AddHours(1);

            
            return Json(htOb, JsonRequestBehavior.AllowGet); //Devuelve un Json para ajax.
        }

        // GET: RoomImage/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RoomImage/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RoomImage/Create
        [HttpPost]
        public ActionResult UploadImage(HttpPostedFileBase file)
        {
            Boolean result = this.Upload(file);
            if (result)
            {
                

                //Si el update funciono borrar la imagen vieja.
            } else
            {

            }
            // after successfully uploading redirect the user
            return RedirectToAction("Index", "RoomImage");
        }

        /// <summary>
        /// Sube una imagen proporcionada por el usuario en el formulario.
        /// Valida que cumpla 2 requisitos: Tamaño y extension.
        /// Sube la imagen a la solucion de cliente y elimina la imagen antigua si todo sale bien.
        /// </summary>
        /// <param name="file">El archivo a subir.</param>
        /// <returns>Si se subio o no, se hace un "and" de los 2 requisitos.</returns>
        private Boolean Upload(HttpPostedFileBase file)
        {
            Boolean fileOK = false, fileSize = false;

            HttpCookie galleta = new HttpCookie("upIm");
            galleta["tr"] = "0";
            
            if (file != null)
            {
                string pic = System.IO.Path.GetFileName(file.FileName);
                
                fileOK = this.fileExtension(pic);

                //Si la extension es admitida.
                if (fileOK)
                {
                    fileSize = this.fileSize(file);
                    //Si el tamano es adecuado.
                    if (fileSize)
                    {
                        string server = HostingEnvironment.ApplicationPhysicalPath;
                        server = server.Replace("Admin", String.Empty);
                        string path = System.IO.Path.Combine(
                                               server, "Content/imagesRoom", pic);
                        try
                        {
                            file.SaveAs(path);
                        }
                        catch (Exception ex)
                        {
                            fileOK = false; //Se pone falso para afectar el return.
                            galleta["tr"] = "3";
                        }
                    } else
                    {
                        //Else de "fileSize"
                        galleta["tr"] = "2";
                    }
                    
                } else
                {
                    //Else de "fileOk"
                    galleta["tr"] = "1";
                }
                
            } else
            {
                //Else de "file != null"
                galleta["tr"] = "-1";
            }

            galleta.Expires = DateTime.Now.AddMinutes(1);
            Response.Cookies.Add(galleta);
            //Si los 2 son true será true, fuera de eso todo será false.
            return fileOK && fileSize;

        }

        /// <summary>
        /// Valida que el tamaño de un archivo sea menor a 2Mb (10^3)
        /// </summary>
        /// <param name="pic">Archivo a subir por el admin.</param>
        /// <returns>Si cumple los criterios para subir o no.</returns>
        private bool fileSize(HttpPostedFileBase pic)
        {
            int size = pic.ContentLength;
            if (size > 0 && size <= 2000000)
                return true;
            else
                return false;
        }
        
        /// <summary>
        /// Valida la extension de un archivo, en este caso son imagenes.
        /// </summary>
        /// <param name="picture">El nombre del archivo que se desea subir.</param>
        /// <returns>Si es admitido o no.</returns>
        private bool fileExtension(String picture)
        {
            bool result = false;

            String fileExtension = System.IO.Path.GetExtension(picture).ToLower();

            String[] allowedExtensions = { ".png", ".jpeg", ".jpg" };

            for (int i = 0; i < allowedExtensions.Length; i++)
            {
                if (fileExtension == allowedExtensions[i])
                {
                    result = true;
                }
            }
            return result;
        }

        // GET: RoomImage/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RoomImage/Edit/5
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

        // GET: RoomImage/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RoomImage/Delete/5
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
