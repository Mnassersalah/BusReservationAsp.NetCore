using BusSystem.Data;
using BusSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusSystem.Services
{
    public class StationRepository : IRepository<Station>
    {
        ApplicationDbContext myDBContext;
        public StationRepository(ApplicationDbContext _myDbContect)
        {
            myDBContext = _myDbContect;
        }

        public void Add(Station entity)
        {
            myDBContext.Stations.Add(entity);
            myDBContext.SaveChanges();
        }

        public Station Details(int id)
        {
            return myDBContext.Stations.FirstOrDefault(t => t.ID == id);
        }

        public List<Station> GetAll()
        {
            return myDBContext.Stations.ToList();
        }

        public void Remove(Station entity)
        {
            myDBContext.Stations.Remove(entity);
            myDBContext.SaveChanges();
        }

        public void Update(Station entity)
        {
            myDBContext.Update(entity);
            myDBContext.SaveChanges();
        }
    }
}
