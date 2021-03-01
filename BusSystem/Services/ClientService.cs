using BusSystem.Data;
using BusSystem.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusSystem.Services
{
    public class ClientService:IRepository<ApplicationUser>
    {
        private readonly ApplicationDbContext context;

        public ClientService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Add(ApplicationUser entity)
        {
           
            
            throw new NotImplementedException();
        }

        public ApplicationUser Details(int id)
        {
            throw new NotImplementedException();
        }

        public List<ApplicationUser> GetAll()
        {
            return context.Users.ToList();
        }

        public void Remove(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }

        public void Update(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }
    }
}
