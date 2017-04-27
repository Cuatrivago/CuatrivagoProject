using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CuatrivagoProjectAdmin.Controllers
{
    public class SignUnController : Controller
    {
        // GET: SignUn
        public ActionResult Index()
        {
            return View();
        }

        // GET: SignUn/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SignUn/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SignUn/Create
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

        // GET: SignUn/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SignUn/Edit/5
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

        // GET: SignUn/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SignUn/Delete/5
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
