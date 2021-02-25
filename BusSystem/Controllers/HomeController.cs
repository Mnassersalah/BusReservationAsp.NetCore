using BusSystem.Models;
using BusSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BusSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository<Station> _stationsService;
        private readonly IRepository<Trip> _tripsService;

        public HomeController(ILogger<HomeController> logger, IRepository<Station> stationsService, IRepository<Trip> tripsService)
        {
            _logger = logger;
            _stationsService = stationsService;
            _tripsService = tripsService;
        }

        public IActionResult Index()
        {
            ViewBag.Stations = new SelectList(_stationsService.GetAll(),"ID","");
            return View();
        }


        public IActionResult Trips(int fromId, int toId, DateTime departureDate, int passengers)
        {

            var trips = _tripsService.GetAll().Where(t => t.StartDateTime.Date == departureDate 
                                                        && t.Route.PickUpID == fromId
                                                        && t.Route.DropOffID== toId
                                                        );

            ViewBag.Passengers = passengers;

            return View(trips);
        }

        public IActionResult TicketBooking(int tripID, int passengers) 
        {
            var trip = _tripsService.Details(tripID);
            if (trip == null)
            {
                return NotFound();
            }
            ViewBag.Passengers = passengers;

            return View(trip);
        }
       




        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
