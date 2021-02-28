using BusSystem.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusSystem.Services
{
    public class ClientService:IRepository<IdentityUser>
    {
        private readonly ApplicationDbContext context;

        public ClientService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Add(IdentityUser entity)
        {
           
            
            throw new NotImplementedException();
        }

        public IdentityUser Details(int id)
        {
            throw new NotImplementedException();
        }

        public List<IdentityUser> GetAll()
        {
            return context.Users.ToList();
        }

        public void Remove(IdentityUser entity)
        {
            throw new NotImplementedException();
        }

        public void Update(IdentityUser entity)
        {
            throw new NotImplementedException();
        }
    }
}
