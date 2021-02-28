using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusSystem.Data;
using BusSystem.Models;
using BusSystem.Services;
using Microsoft.AspNetCore.Authorization;

namespace BusSystem.Controllers
{
    [Authorize(Roles = "Employee")]
    public class BusesController : Controller
    {
        private readonly IRepository<Bus> busService;

        public BusesController(IRepository<Bus> busService)
        {
            this.busService = busService;
        }

        // GET: Buses
        public IActionResult Index(string sortOrder)
        {
            var buses = busService.GetAll();
            /*
            ViewBag.CategorySortParm = String.IsNullOrEmpty(sortOrder) ? "category_desc" : "";
            ViewBag.CapacitySortParm = sortOrder == "capacity" ? "capacity_desc" : "capacity";
            switch (sortOrder)
            {
                case "category_desc":
                    buses = buses.OrderByDescending(s => s.Category).ToList();
                    break;
                case "capacity":
                    buses = buses.OrderBy(s => s.Capacity).ToList();
                    break;
                case "capacity_desc":
                    buses = buses.OrderByDescending(s => s.Capacity).ToList();
                    break;
                default:
                    buses = buses.OrderBy(s => s.Category).ToList();
                    break;
            }*/ 
            return View(buses);

        }

        // GET: Buses/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bus = busService.Details((int)id);
            if (bus == null)
            {
                return NotFound();
            }

            return View(bus);
        }

        // GET: Buses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Buses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ID,BusNum,Category,Capacity")] Bus bus)
        {
            if (ModelState.IsValid)
            {
                busService.Add(bus);
                return RedirectToAction(nameof(Index));
            }
            return View(bus);
        }

        // GET: Buses/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bus = busService.Details((int)id);
            if (bus == null)
            {
                return NotFound();
            }
            return View(bus);
        }

        // POST: Buses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ID,BusNum,Category,Capacity")] Bus bus)
        {
            if (id != bus.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    busService.Update(bus);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BusExists(bus.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(bus);
        }

        // GET: Buses/Delete/5
        public  IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bus = busService.Details((int)id);
            if (bus == null)
            {
                return NotFound();
            }

            ViewBag.Message = null;


            // if (Routes.checkRoute(station.ID) == null)
            if (bus.Trips.Count != 0)
            {
                ViewBag.Message = " You can't Romove This bus Because IT has Trips ";

            }

            return View(bus);
        }

        // POST: Buses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var bus = busService.Details(id);
            if (bus.Trips.Count == 0)
            {
                busService.Remove(bus);
            }
            
            return RedirectToAction(nameof(Index));
        }

        private bool BusExists(int id)
        {
            return busService.GetAll().Any(e => e.ID == id);
        }
    }
}
