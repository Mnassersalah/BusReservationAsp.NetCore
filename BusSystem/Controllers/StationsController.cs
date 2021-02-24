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
    public class StationsController : Controller
    {
       
        IRepository<Station> stationRepo;
        public StationsController(IRepository<Station> _stationRepo)
        {
            stationRepo = _stationRepo;
        }



        // GET: Stations
        public IActionResult Index()
        {
            return View(stationRepo.GetAll());
        }

        // GET: Stations/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var station = stationRepo.Details(id ?? 0);
            if (station == null)
            {
                return NotFound();
            }

            return View(station);
        }

        // GET: Stations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Stations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ID,City,Name")] Station station)
        {
            if (ModelState.IsValid)
            {
                stationRepo.Add(station);
                return RedirectToAction(nameof(Index));
            }
            return View(station);
        }

        // GET: Stations/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var station = stationRepo.Details(id ?? 0);
            if (station == null)
            {
                return NotFound();
            }
            return View(station);
        }

        // POST: Stations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ID,City,Name")] Station station)
        {
            if (id != station.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    stationRepo.Update(station);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StationExists(station.ID))
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
            return View(station);
        }

        // GET: Stations/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var station = stationRepo.Details(id ?? 0);
            if (station == null)
            {
                return NotFound();
            }

            return View(station);
        }

        // POST: Stations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {

            var station = stationRepo.Details(id);
            stationRepo.Remove(station);

            return RedirectToAction(nameof(Index));
        }

        private bool StationExists(int id)
        {
            return stationRepo.Details(id) == null;
        }
    }
}
