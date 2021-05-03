﻿using BusSystem.Data;
using BusSystem.Models;
using BusSystem.Services;
using Microsoft.EntityFrameworkCore;
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
            return context.Routes.Include(r=>r.DropOff)
                                 .Include(r=>r.PickUp)
                                 .Include(r => r.Trips)
                                 .ToList();
        }

        public Route Details(int id)
        {
            return context.Routes.Include("PickUp")
                                 .Include("DropOff")
                                 .Include("Trips")
                                 .FirstOrDefault(r=> r.ID==id);
        }
        //public Route checkRoute(int id)
        //{
            
        //    var router= context.Routes.FirstOrDefault(r => r.DropOff.ID == id);
        //    if(router==null)
        //        router = context.Routes.FirstOrDefault(r => r.PickUp.ID == id);
        //    return router;
        //}

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

