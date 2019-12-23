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
    public class ClientRepository : IClientRepository
    {
        private DB_Context db;

        public ClientRepository(DB_Context context)
        {
            this.db = context;
        }

        public IEnumerable<Clients> GetAll()
        {
            return db.Set<Clients>().ToList();
        }

        public Clients Get(int id)
        {
            return db.Set<Clients>().Find(id);
        }


        public Clients Create(Clients client)
        {
            client.Id = Guid.NewGuid().ToString();
            db.Set<Clients>().Add(client);
            db.SaveChangesAsync();
            return client;

        }

        public Clients Update(Clients client)
        {
            db.Entry(client).State = EntityState.Modified;
            db.SaveChangesAsync();
            return client;
        }

        public void Delete(int id)
        {
            var client = db.Set<Clients>().Find(id);
            if (client == null)
            {
                throw new Exception("Нечего удалять");
            }
            else
            {
                db.Set<Clients>().Remove(client);
                db.SaveChangesAsync();
            }
        }
    }
}
  