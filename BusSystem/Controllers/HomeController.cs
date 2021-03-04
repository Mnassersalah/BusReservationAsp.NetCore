﻿using BusSystem.Models;
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
using Microsoft.AspNetCore.Authorization;

namespace BusSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _user;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository<Station> _stationsService;
        private readonly IRepository<Trip> _tripsService;
        private readonly IRepository<Ticket> _ticketService;

        public HomeController(ILogger<HomeController> logger, IRepository<Station> stationsService, IRepository<Trip> tripsService, IRepository<Ticket> ticketService,UserManager<ApplicationUser> user , SignInManager<ApplicationUser> signInManager)
        {
            _user = user;
            this.signInManager = signInManager;
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
                                                        && t.StartDateTime >= DateTime.Now
                                                        && t.Route.PickUpID == fromId
                                                        && t.Route.DropOffID== toId
                                                        && passengers <= t.AvailableSeatsArray.Length);
            
            ViewBag.Passengers = passengers;
            ViewBag.dropoff = _stationsService.Details(toId);
            ViewBag.pickUp = _stationsService.Details(fromId);
            ViewBag.departureDate = departureDate;
            return View(trips);
        }

        [Authorize]
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
            

            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            string setsstring = String.Join(",", sets);
            Ticket t = new Ticket() { ClientID = userId, PassengerCount = sets.Length, TripID = tripID, BookedSeats = setsstring };

            _ticketService.Add(t);
            //create taiket

            // update available seat
            Trip tr = _tripsService.Details(t.TripID);

            //
            string[] available = tr.AvailableSeats.Split(",").Except(sets).ToArray();
            tr.AvailableSeats = String.Join(",", available);
            _tripsService.Update(tr);
            //userID


            ViewBag.sets = sets;
            

            return View(t);
        }

        [HttpPost]
        public IActionResult DeleteTicket(int id , string[] sets)
        {

            if (_tripsService.Details(_ticketService.Details(id).TripID).StartDateTime < DateTime.Now)
            {
                return RedirectToAction(nameof(Index));
            }
            Trip t = _tripsService.Details(_ticketService.Details(id).TripID);
            
            foreach(var i in sets)
            {
                t.AvailableSeats += "," + i;
            }
            _tripsService.Update(t);
            _ticketService.Remove(_ticketService.Details(id));

            
            return RedirectToAction(nameof(Index));

        }


        //User Tickets management
        public IActionResult UserTickets()
        {
            string userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            return View(_ticketService.GetAll().Where(t => t.ClientID == userId));
        }

        [HttpPost]
        public IActionResult DeleteUserTicket(int id)
        {
            Ticket ticket = _ticketService.Details(id);
            if (_tripsService.Details(ticket.TripID).StartDateTime < DateTime.Now)
            {
                return RedirectToAction(nameof(UserTickets));
            }
            Trip t = _tripsService.Details(_ticketService.Details(id).TripID);

            t.AvailableSeats += "," + ticket.BookedSeats;
            _tripsService.Update(t);
            _ticketService.Remove(ticket);


            return RedirectToAction(nameof(UserTickets));

        }


        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(Index));
        }



        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contacts()
        {
            return View();
        }

        public IActionResult FAQ()
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
