using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AspNetMvc_001.Data;
using AspNetMvc_001.Models;

namespace AspNetMvc_001.Controllers
{
    public class ShopController : Controller
    {

        public SampleContext Context = new SampleContext();
        
        // GET: Shop
        public ActionResult Index()
        {
            var customers = Context.Orders.AsQueryable();
            return View(customers);
        }

        // GET: Shop/Details/5
        public ActionResult Details(int id)
        {
            Order2 order = Context.Orders.Find(id);
            return View(order);
        }

        // GET: Shop/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Shop/Create
        [HttpPost]
        public ActionResult Create(Order2 order)
        {
            try
            {
                // TODO: Add insert logic here

                Context.Orders.Add(order);
                Context.Entry<Order2>(order).State = EntityState.Added;
                Context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Shop/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Shop/Edit/5
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

        // GET: Shop/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Shop/Delete/5
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
