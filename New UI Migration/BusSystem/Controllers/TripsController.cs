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
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text;
using Microsoft.AspNetCore.Authorization;


namespace BusSystem.Controllers
{
    //[Authorize(Roles = "Admin,Employee")]

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

        #region CRUD

        public IActionResult Index()
        {
            return View(_tripService.GetAll());
        }

        public IActionResult Details(int? id)
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
        public IActionResult Create([Bind("ID,StartDateTime,Price,AvailableSeats,RouteID,BusID")] Trip trip)
        {
            this.ValidateTrip(trip);
            if (ModelState.IsValid)
            {
                
                Trip.GenerateAvailableSeats(trip, _busService.Details(trip.BusID).Capacity);
                _tripService.Add(trip);

                return RedirectToAction(nameof(Index));
            }

            this.Get_ForeignKey(trip);
            return View(trip);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var trip = _tripService.Details((int)id);
            if (trip == null)
                return NotFound();

            this.CanEdit(trip);
            this.Get_ForeignKey(trip);
            return View(trip);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ID,StartDateTime,Price,AvailableSeats,RouteID,BusID")] Trip trip)
        {
            if (id != trip.ID)
                return NotFound();

/*
            if (_tripService.Details(id).BusID != trip.BusID)
                this.ValidateBus(trip.BusID, trip.StartDateTime);*/

            this.ValidateDate(trip.StartDateTime);

            if (ModelState.IsValid)
            {
                try
                {
                    //Trip.GenerateAvailableSeats(trip, _busService.Details(trip.BusID).Capacity);


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

        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var trip = _tripService.Details((int)id);
            if (trip == null)
                return NotFound();

            this.CanEdit(trip);
            return View(trip);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Trip trip = _tripService.Details(id);
            this.ValidateDate(trip.StartDateTime);

            if (ModelState.IsValid)
            {
                if (trip.Tickets == null)
                {
                    _tripService.Remove(trip);
                }
                return RedirectToAction(nameof(Index));
            }

            return View(trip);
        }

        #endregion

        #region Methods

        #region ViewBags

        private void Get_ForeignKey(Trip trip)
        {
            ViewData["BusID"] = new SelectList(_busService.GetAll(), "ID", "BusNum", trip.BusID);
            ViewData["RouteID"] = new SelectList(_routeService.GetAll(), "ID", "", trip.RouteID);
        }

        private void Get_ForeignObjects()
        {
            ViewData["BusID"] = new SelectList(_busService.GetAll(), "ID", "BusNum");
            ViewData["RouteID"] = new SelectList(_routeService.GetAll(), "ID", "");
        }

        #endregion

        #region Validation

        private void ValidateTrip(Trip trip)
        {
            this.ValidateDate(trip.StartDateTime);
            this.ValidateBus(trip.BusID, trip.StartDateTime);
        }




        private void ValidateDate(DateTime tripDate)
        {
            if (tripDate <= DateTime.Now)
            {
                ModelState.AddModelError("StartDateTime", "Date is not applicable");
            }
        }

        private void ValidateBus(int BusID, DateTime tripDate)
        {
            Bus _bus = _busService.Details(BusID);
            if (_bus.Trips.Where(t => t.StartDateTime <= tripDate && t.ReturnDateTime >= tripDate).Count() != 0)
            {
                ModelState.AddModelError("BusID", "Bus is not avaliable at that time");
            }
        }

        #endregion

        private void CanEdit(Trip trip)
        {
            string msg = "You can't edit or delete this trip";
            ViewBag.Message = null;

            if (trip.Tickets != null)
            {
                ViewBag.Message = msg;
            }

            if (trip.AvailableSeatsCount != null)
            {
                int AvailableSeats = Convert.ToInt32(trip.AvailableSeatsCount);
                if (AvailableSeats != trip.Bus.Capacity)
                {
                    ViewBag.Message = msg;
                }
            }

            if (trip.StartDateTime <= DateTime.Now)
            {
                ViewBag.Message = msg;
            }
        }

        #endregion
    }
}
