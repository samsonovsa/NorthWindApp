using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NorthWindApp.Controllers
{
    public class SupplierController : Controller
    {
        // GET: Supplier
        public ActionResult Index()
        {
            return View();
        }

        // GET: Supplier/Details/5
        public ActionResult Details(int id)
        {
            throw new NotImplementedException();
        }

        // GET: Supplier/Create
        public ActionResult Create()
        {
            throw new NotImplementedException();
        }

        // POST: Supplier/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                throw new NotImplementedException();
            }
        }

        // GET: Supplier/Edit/5
        public ActionResult Edit(int id)
        {
            throw new NotImplementedException();
        }

        // POST: Supplier/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                throw new NotImplementedException();
            }
        }

        // GET: Supplier/Delete/5
        public ActionResult Delete(int id)
        {
            throw new NotImplementedException();
        }

        // POST: Supplier/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                throw new NotImplementedException();
            }
        }
    }
}