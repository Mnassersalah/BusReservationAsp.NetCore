using BusSystem.Data;
using BusSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusSystem.Services
{
    public class BusesService : IRepository<Bus>
    {
        private readonly ApplicationDbContext context;

        public BusesService(ApplicationDbContext context)
        {
            this.context = context;
        }


        public List<Bus> GetAll()
        {
            return context.Buses.ToList();
        }

        public Bus Details(int id)
        {
            return context.Buses.Include(b=>b.Trips)
                                .ThenInclude(t=>t.Route)
                                .Include("Trips.Route.PickUp")
                                .Include("Trips.Route.DropOff")
                                .FirstOrDefault(b => b.ID == id);
        }
        public void Add(Bus entity)
        {
            context.Buses.Add(entity);
            context.SaveChanges();
        }

        public void Update(Bus entity)
        {
            context.Buses.Update(entity);
            context.SaveChanges();
        }
        public void Remove(Bus entity)
        {
            context.Buses.Remove(entity);
            context.SaveChanges();
        }
    }
}
