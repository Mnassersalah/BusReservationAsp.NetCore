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
    [Authorize(Roles = "Admin,Employee")]
    public class RoutesController : Controller
    {
        private readonly IRepository<Route> Routes;
        private readonly IRepository<Station> stationRepo;

        public RoutesController(IRepository<Route> _Routes, IRepository<Station> _stationRepo)
        {
            this.Routes = _Routes;
            this.stationRepo = _stationRepo;
        }

        // GET: Routes
        public IActionResult Index()
        {
            //var applicationDbContext = _context.Routes.Include(r => r.DropOff).Include(r => r.PickUp);
            //return View(await applicationDbContext.ToListAsync());
            return View(Routes.GetAll());
        }

        // GET: Routes/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var routeR = Routes.Details((int)id);
            if (routeR == null)
            {
                return NotFound();
            }

            return View(routeR);
        }

        // GET: Routes/Create
        public IActionResult Create()
        {
            ViewData["DropOffID"] = new SelectList(stationRepo.GetAll(), "ID", "tostringProp");
            ViewData["PickUpID"] = new SelectList(stationRepo.GetAll(), "ID", "tostringProp");
            return View();
        }

        // POST: Routes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ID,Duration,PickUpID,DropOffID")] Route route)
        {

            if (route.PickUpID == route.DropOffID)
            {
                ModelState.AddModelError("DropOffID", "Dropoff station must be different from PickUp Station");
            }

            if (ModelState.IsValid)
            {
                Routes.Add(route);
                return RedirectToAction(nameof(Index));
            }


            ViewData["DropOffID"] = new SelectList(stationRepo.GetAll(), "ID", "tostringProp");
            ViewData["PickUpID"] = new SelectList(stationRepo.GetAll(), "ID", "tostringProp");
            return View(route);
        }

        // GET: Routes/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Rroute = Routes.Details((int)id);
            if (Rroute == null)
            {
                return NotFound();
            }
            ViewData["DropOffID"] = new SelectList(stationRepo.GetAll(), "ID", "tostringProp");
            ViewData["PickUpID"] = new SelectList(stationRepo.GetAll(), "ID", "tostringProp");
            return View(Rroute);
        }

        // POST: Routes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ID,Duration,PickUpID,DropOffID")] Route route)
        {
            if (id != route.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Routes.Update(route);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RouteExists(route.ID))
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
            ViewData["DropOffID"] = new SelectList(stationRepo.GetAll(), "ID", "tostringProp");
            ViewData["PickUpID"] = new SelectList(stationRepo.GetAll(), "ID", "tostringProp");
            return View(route);
        }

        // GET: Routes/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var route = await _context.Routes
            //    .Include(r => r.DropOff)
            //    .Include(r => r.PickUp)
            //    .FirstOrDefaultAsync(m => m.ID == id);
            var RRout = Routes.Details((int)id);
            if (RRout == null)
            {
                return NotFound();
            }

            ViewBag.Message = null;


            // if (Routes.checkRoute(station.ID) == null)
            if (RRout.Trips.Count != 0)
            {
                ViewBag.Message = "You can't romove this route because it has trips";

            }

            return View(RRout);
        }

        // POST: Routes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            //var route = await _context.Routes.FindAsync(id);
            //_context.Routes.Remove(route);
            //await _context.SaveChangesAsync();
            var RRout = Routes.Details((int)id);
            if (RRout.Trips.Count == 0)
            {
                Routes.Remove(Routes.Details(id));
            }
            return RedirectToAction(nameof(Index));
        }

        private bool RouteExists(int id)
        {
            return Routes.GetAll().Any(e => e.ID == id);
        }
    }
}
