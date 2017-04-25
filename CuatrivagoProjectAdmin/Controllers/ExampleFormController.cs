using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CuatrivagoProjectAdmin.Controllers
{
    public class ExampleFormController : Controller
    {
        // GET: ExampleForm
        public ActionResult Index()
        {
            return View();
        }

        // GET: ExampleForm/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ExampleForm/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ExampleForm/Create
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

        // GET: ExampleForm/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ExampleForm/Edit/5
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

        // GET: ExampleForm/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ExampleForm/Delete/5
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
