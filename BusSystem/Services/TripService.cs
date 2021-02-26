using BusSystem.Data;
using BusSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusSystem.Services
{
    public class TripService : IRepository<Trip>
    {
        private readonly ApplicationDbContext _context;

        public TripService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Trip> GetAll()
        {
            return _context.Trips.Include(t => t.Bus)
                                 .Include("Route")
                                 .Include("Route.DropOff")
                                 .Include("Route.PickUp")
                                 .ToList();//.Include("Tickets")
        }

        public Trip Details(int id)
        {
            return _context.Trips.Include("Bus")
                                 .Include("Tickets")
                                 .Include("Route")
                                 .Include("Route.DropOff")
                                 .Include("Route.PickUp")
                                 .FirstOrDefault(m => m.ID == id);

        }

        public void Add(Trip trip)
        {
            _context.Add(trip);
            _context.SaveChanges();
        }

        public void Update(Trip trip)
        {
            _context.Update(trip);
            _context.SaveChanges();
        }

        public void Remove(Trip trip)
        {
            _context.Trips.Remove(trip);
            _context.SaveChanges();
        }

        /*public string GetSeats(int BusID)
        {
            int seatsCount = _context.Buses.Where(b => b.ID == BusID).FirstOrDefault().Capacity;

            string seats = null;
            if (seatsCount >= 1)
            {
                seats = "1";
                for (int i = 2; i <= seatsCount; i++)
                    seats += "," + i;
            }
            return seats;
        }*/
    }
}
