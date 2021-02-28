using BusSystem.Data;
using BusSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusSystem.Services
{
    public class TicketService : IRepository<Ticket>
    {
        private readonly ApplicationDbContext ctx;

        public TicketService(ApplicationDbContext ctx)
        {
            this.ctx = ctx;
        }
        public void Add(Ticket entity)
        {
            ctx.Tickets.Add(entity);
            ctx.SaveChanges();
        }

        public Ticket Details(int id)
        {
            return ctx.Tickets.Include(t => t.Client).Include(t => t.Trip).Include("Trip.Route.PickUp").Include("Trip.Route.DropOff").FirstOrDefault(m => m.ID == id);
        }

        public List<Ticket> GetAll()
        {
            return ctx.Tickets.Include(t=>t.Client).Include(t => t.Trip).Include("Trip.Route.PickUp").Include("Trip.Route.DropOff").ToList();
        }

        public void Remove(Ticket entity)
        {
            ctx.Tickets.Remove(entity);
            ctx.SaveChanges();
        }

        public void Update(Ticket entity)
        {
            ctx.Tickets.Update(entity);
            ctx.SaveChanges();
        }
    }
}
