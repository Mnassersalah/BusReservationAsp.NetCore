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

namespace BusSystem.Controllers
{
    public class TripsController : Controller
    {
        private readonly IRepository<Trip> _tripService;
        private readonly IRepository<Bus> _busService;
        private readonly IRepository<Route> _routeService;

        public TripsController(IRepository<Trip> tripService, IRepository<Bus> busService, IRepository<Route> routeService)
        {
            _tripService = tripService;
            _busService = busService;
            _routeService = routeService;
        }

        public async Task<IActionResult> Index()
        {
            return View(_tripService.GetAll());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var trip = _tripService.Details((int)id);
            if (trip == null)
                return NotFound();

            return View(trip);
        }

        public IActionResult Create()
        {
            this.Get_ForeignObjects();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,StartDateTime,Price,AvailableSeats,RouteID,BusID")] Trip trip)
        {
            if (ModelState.IsValid)
            {
                _tripService.Add(trip);
                return RedirectToAction(nameof(Index));
            }

            this.Get_ForeignKey(trip);
            return View(trip);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var trip = _tripService.Details((int)id);
            if (trip == null)
                return NotFound();

            this.Get_ForeignKey(trip);

            return View(trip);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,StartDateTime,Price,AvailableSeats,RouteID,BusID")] Trip trip)
        {
            if (id != trip.ID)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _tripService.Update(trip);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }

            this.Get_ForeignKey(trip);

            return View(trip);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var trip = _tripService.Details((int)id);
            if (trip == null)
                return NotFound();
            ViewBag.Message = null;


            // if (Routes.checkRoute(station.ID) == null)
            if (trip.Tickets.Count != 0)
            {
                ViewBag.Message = " You can't Romove This trip Because IT has Tickets ";

            }

            return View(trip);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Trip trip)
        {
            if (trip.Tickets.Count == 0)
            {
                _tripService.Remove(trip);
            }
           
            return RedirectToAction(nameof(Index));
        }

        private void Get_ForeignKey(Trip trip)
        {
            ViewData["BusID"] = new SelectList(_busService.GetAll(), "ID", "ID", trip.BusID);
            ViewData["RouteID"] = new SelectList(_routeService.GetAll(), "ID", "ID", trip.RouteID);
        }

        private void Get_ForeignObjects()
        {
            ViewData["BusID"] = new SelectList(_busService.GetAll(), "ID", "Category");
            ViewData["RouteID"] = new SelectList(_routeService.GetAll(), "ID");
        }
    }
}
