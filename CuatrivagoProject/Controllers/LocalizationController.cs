using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CuatrivagoProject.Controllers
{
    public class LocalizationController : Controller
    {
        // GET: Localization
        public ActionResult Index()
        {
            return View();
        }

        // GET: Localization/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Localization/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Localization/Create
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

        // GET: Localization/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Localization/Edit/5
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

        // GET: Localization/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Localization/Delete/5
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
