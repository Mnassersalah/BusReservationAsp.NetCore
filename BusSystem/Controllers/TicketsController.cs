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
using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Authorization;

namespace BusSystem.Controllers
{


    namespace BusSystem.Controllers
    {
        [Authorize(Roles = "Employee")]
        public class TicketsController : Controller
        {
            private readonly IRepository<Ticket> tickets;
            private readonly IRepository<Trip> trips;
            private readonly IRepository<ApplicationUser> users;

            public TicketsController(IRepository<Ticket> tickets, IRepository<Trip> trips, IRepository<ApplicationUser> users)
            {
                this.tickets = tickets;
                this.trips = trips;
                this.users = users;
            }

            // GET: Tickets
            public ActionResult Index()
            {
                return View(tickets.GetAll());
            }

            // GET: Tickets/Details/5
            public ActionResult Details(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var t = tickets.Details(id ?? 0);


                if (t == null)
                {
                    return NotFound();
                }

                return View(t);
            }

            // GET: Tickets/Create
            public IActionResult Create()
            {
                ViewData["TripID"] = new SelectList(trips.GetAll(), "ID", "tostringprop");
                ViewData["ClientID"] = new SelectList(users.GetAll(), "Id", "UserName");

                return View();
            }


            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Create([Bind("ID,PassengerCount,BookedSeats,TripID,ClientID")] Ticket ticket)
            {
                //Make Sure!!
                //Check for seats availability
                if (ticket.PassengerCount > ((trips.Details(ticket.TripID).AvailableSeats)?.Length ?? 0 / 2))
                {
                    ModelState.AddModelError("PassengerCount", $"There are not available seats for {ticket.PassengerCount} passengers ");
                }

                //
                //Check for date 
                if ((trips.Details(ticket.TripID)?.StartDateTime < DateTime.Now))
                {
                    ModelState.AddModelError("TripID", "This trip is out of date");
                }
                if (ModelState.IsValid)
                {
                    tickets.Add(ticket);
                    return RedirectToAction(nameof(Index));
                }
                ViewData["TripID"] = new SelectList(trips.GetAll(), "ID", "tostringprop");
                ViewData["ClientID"] = new SelectList(users.GetAll(), "Id", "UserName");
                return View(ticket);
            }

            // GET: Tickets/Edit/5
            #region Edit Useless
            //public ActionResult Edit(int? id)
            //{
            //    if (id == null)
            //    {
            //        return NotFound();
            //    }

            //    var t = tickets.Details((int)id);
            //    if (t == null)
            //    {
            //        return NotFound();
            //    }
            //    ViewData["TripID"] = new SelectList(trips.GetAll(), "ID", "tostringprop", t.TripID);
            //    ViewData["ClientID"] = new SelectList(users.GetAll(),"Id", "UserName", t.ClientID);
            //    return View(t);
            //}

            //[HttpPost]
            //[ValidateAntiForgeryToken]
            //public ActionResult Edit(int id, [Bind("ID,PassengerCount,BookedSeats,TripID,ClientID")] Ticket ticket)
            //{
            //    if (id != ticket.ID)
            //    {
            //        return NotFound();
            //    }

            //    //Check for date 
            //    if ((trips.Details(ticket.TripID)?.StartDateTime > DateTime.Now))
            //    {
            //        ModelState.AddModelError("TripID", "This trip is out of date");
            //    }
            //    if (ticket.PassengerCount > ((trips.Details(ticket.TripID).AvailableSeats)?.Length ?? 0 / 2))
            //    {
            //        ModelState.AddModelError("PassengerCount", $"There are not available seats for {ticket.PassengerCount} passengers ");
            //    }


            //    if (ModelState.IsValid)
            //    {
            //        try
            //        {
            //            tickets.Update(ticket);
            //        }
            //        catch (DbUpdateConcurrencyException)
            //        {
            //            if (!TicketExists(ticket.ID))
            //            {
            //                return NotFound();
            //            }
            //            else
            //            {
            //                throw;
            //            }
            //        }
            //        return RedirectToAction(nameof(Index));
            //    }
            //    ViewData["ClientID"] = new SelectList(users.GetAll(),"Id", "UserName");
            //    ViewData["TripID"] = new SelectList(trips.GetAll(),"ID", "tostringprop");
            //    return View(ticket);
            //} 
            #endregion

            // GET: Tickets/Delete/5
            public ActionResult Delete(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var t = tickets.Details((int)id);

                if (t == null)
                {
                    return NotFound();
                }
                ViewBag.Date = trips.Details(tickets.Details(id ?? 0).TripID).StartDateTime;

                return View(t);
            }

            // POST: Tickets/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public IActionResult DeleteConfirmed(int id)
            {

                if (trips.Details(tickets.Details(id).TripID).StartDateTime > DateTime.Now)
                {
                    return RedirectToAction(nameof(Index));
                }
                tickets.Remove(tickets.Details(id));
                return RedirectToAction(nameof(Index));

            }

            private bool TicketExists(int id)
            {
                return tickets.GetAll().Any(e => e.ID == id);
            }
        }
    }
}

