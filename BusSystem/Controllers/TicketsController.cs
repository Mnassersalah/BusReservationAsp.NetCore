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

namespace BusSystem.Controllers
{
    public class TicketsController : Controller
    {
        private readonly IRepository<Ticket> tickets;
        private readonly IRepository<Trip> trips;
        private readonly IRepository<IdentityUser> users;

        public TicketsController(IRepository<Ticket> tickets ,IRepository<Trip> trips, IRepository<IdentityUser> users)
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

            var t = tickets.Details(id??0);


            if (t == null)
            {
                return NotFound();
            }

            return View(t);
        }

        // GET: Tickets/Create
        public IActionResult Create()
        {
            ViewData["TripID"] = new SelectList(trips.GetAll(), "ID");
            ViewData["ClientID"] = new SelectList(users.GetAll(), "Id", "Id");

            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ID,PassengerCount,BookedSeats,TripID,ClientID")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                tickets.Add(ticket);
                return RedirectToAction(nameof(Index));
            }
            ViewData["TripID"] = new SelectList(trips.GetAll(), "ID", "ID");
            ViewData["ClientID"] = new SelectList(users.GetAll(), "Id", "Id");
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public ActionResult Edit(int? id)
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
            ViewData["TripID"] = new SelectList(trips.GetAll(), "ID", "ID",t.TripID);
            ViewData["ClientID"] = new SelectList(users.GetAll(),"Id", "Id",t.ClientID);
            return View(t);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("ID,PassengerCount,BookedSeats,TripID,ClientID")] Ticket ticket)
        {
            if (id != ticket.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    tickets.Update(ticket);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.ID))
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
            ViewData["ClientID"] = new SelectList(users.GetAll(), "Id", "Id");
            ViewData["TripID"] = new SelectList(trips.GetAll(), "ID", "ID");
            return View(ticket);
        }

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

            return View(t);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            tickets.Remove(tickets.Details(id));
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(int id)
        {
            return tickets.GetAll().Any(e => e.ID == id);
        }
    }
}
