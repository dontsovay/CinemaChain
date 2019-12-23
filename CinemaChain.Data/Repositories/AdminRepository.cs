using CinemaChain.Data.Context;
using CinemaChain.Data.Interfaces.Repositories;
using CinemaChain.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityState = Microsoft.EntityFrameworkCore.EntityState;

namespace CinemaChain.Data.Repositories
{
    public class AdminRepository: IAdminRepository
    {
        private DB_Context db;

        public AdminRepository(DB_Context context) 
        {
            this.db = context;
        }

        public IEnumerable<Admins> GetAll()
        {
            return db.Set<Admins>().ToList();
        }

        public Admins Get(int id)
        {
            return db.Set<Admins>().Find(id);
        }


    public Admins GetAdmin(string username)
        {
            return db.Admins.Where(u => u.Users.UserName.Equals(username)).First();
        }

        public Admins Create(Admins admin)
        {
            admin.Id = Guid.NewGuid().ToString();
            db.Set<Admins>().Add(admin);
            db.SaveChangesAsync();
            return admin;

        }

        public Admins Update(Admins admin)
        {
            db.Entry(admin).State = EntityState.Modified;
            db.SaveChangesAsync();
            return admin;
        }

        public void Delete(int id)
        {
            var admin = db.Set<Admins>().Find(id);
            if (admin == null)
            {
                throw new Exception("Нечего удалять");
            }
            else
            {
                db.Set<Admins>().Remove(admin);
                db.SaveChangesAsync();
            }
        }
    }
}
