using CinemaChain.Data.Context;
using CinemaChain.Data.Interfaces.Repositories;
using CinemaChain.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CinemaChain.Data.Repositories
{
    public class OwnerRepository : IOwnerRepository
    {
        private DB_Context db;

        public OwnerRepository(DB_Context context) 
        {
            this.db = context;
        }

        public IEnumerable<Owners> GetAll()
        {
            return db.Set<Owners>().ToList();
        }

        public Owners Get(int id)
        {
            return db.Set<Owners>().Find(id);
        }

        public Owners Create(Owners owner)
        {
            owner.Id = Guid.NewGuid().ToString();
            db.Set<Owners>().Add(owner);
            db.SaveChangesAsync();
            return owner;

        }

        public Owners Update(Owners owner)
        {
            db.Entry(owner).State = EntityState.Modified;
            db.SaveChangesAsync();
            return owner;
        }

        public void Delete(int id)
        {
            var owner = db.Set<Owners>().Find(id);
            if (owner == null)
            {
                throw new Exception("Нечего удалять");
            }
            else
            {
                db.Set<Owners>().Remove(owner);
                db.SaveChangesAsync();
            }
        }

    }
}
