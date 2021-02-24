using BusSystem.Data;
using BusSystem.Models;
using BusSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusSystem.Services
{
    public class RoutesService : IRepository<Route>
    {
        private readonly ApplicationDbContext context;
        public RoutesService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public List<Route> GetAll()
        {
            return context.Routes.ToList();
        }

        public Route Details(int id)
        {
            return context.Routes.Find(id);
        }

        public void Add(Route rote)
        {
            context.Routes.Add(rote);
            context.SaveChanges();
        }

        public void Update( Route rote)
        {
            context.Routes.Update(rote);
           

            context.SaveChanges();
        }

        public void Remove(Route rote)
        {
            context.Remove(rote);
            context.SaveChanges();
        }
    }
}

