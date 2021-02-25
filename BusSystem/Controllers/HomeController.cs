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
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace BusSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<IdentityUser> _user;
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository<Station> _stationsService;
        private readonly IRepository<Trip> _tripsService;
        private readonly IRepository<Ticket> _ticketService;

        public HomeController(ILogger<HomeController> logger, IRepository<Station> stationsService, IRepository<Trip> tripsService, IRepository<Ticket> ticketService,UserManager<IdentityUser> user)
        {
            _user = user;
            _logger = logger;
            _stationsService = stationsService;
            _tripsService = tripsService;
            _ticketService = ticketService;
        }

        public IActionResult Index()
        {
            ViewBag.Stations = new SelectList(_stationsService.GetAll(),"ID","");
            return View();
        }


        public IActionResult Trips(int fromId, int toId, DateTime departureDate, int passengers)
        {


            if (toId == fromId)
            {
                ModelState.AddModelError("toId", "Dropoff station must be different from PickUp Station");
            }

            if (departureDate < DateTime.Now)
            {
                ModelState.AddModelError("departureDate", "This trip is out of date");
            }


            var trips = _tripsService.GetAll().Where(t => t.StartDateTime.Date == departureDate 
                                                        && t.Route.PickUpID == fromId
                                                        && t.Route.DropOffID== toId
                                                        && passengers <= t.AvailableSeatsArray.Length);
            
            ViewBag.Passengers = passengers;
            ViewBag.dropoff = _stationsService.Details(toId);
            ViewBag.pickUp = _stationsService.Details(fromId);
            ViewBag.departureDate = departureDate;
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
            ViewBag.availableSets = trip.AvailableSeats.Split(",");
           
            return View(trip);
        }
       


        public IActionResult ticketBookseats(int tripID,string[] sets)
        {

            //userID
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            string setsstring = String.Join(",", sets);
            Ticket t = new Ticket() { ClientID = userId, PassengerCount = sets.Length, TripID = tripID, BookedSeats = setsstring };
            _ticketService.Add(t);
            //create taiket

            // update available set
            Trip tr = _tripsService.Details(tripID);
            
            //
            string[] available =  tr.AvailableSeats.Split(",").Except(sets).ToArray();
            tr.AvailableSeats = String.Join(",", available);
            _tripsService.Update(tr);

            return RedirectToAction("Index");
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
