using CinemaChain.Data.Context;
using CinemaChain.Data.Interfaces.Repositories;
using CinemaChain.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CinemaChain.Data.Repositories
{
    public class UserRepository:  IUserRepository
    {
        private DB_Context db;

        public UserRepository(DB_Context context) 
        {
            this.db = context;
        }
        public IEnumerable<Users> GetAll()
        {
           return db.Users;
        }
        public Users GetUser(string username)
        {
            return db.Users.Where(name => name.UserName.Equals(username)).First();
        }
        public string HASH(string password)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(password));

            return Convert.ToBase64String(hash);
        }
        public Users CustomGet(string username, string password)
        {

            return db.Users.Where(name => name.UserName.Equals(username)).Where(pass => pass.Password.Equals(HASH(password))).First();
        }
        public Users Get(string username)
        {
            return db.Users.Where(i => i.UserName.Equals(username)).FirstOrDefault();
        }

        public Users Create(Users user)
        {

            db.Entry(user).State = EntityState.Added;
            db.SaveChangesAsync();
            return user;

        }

        public Users Update(Users user)
        {

            db.Entry(user).Property(c => c.Password).IsModified = true;
            db.SaveChangesAsync();
            return user;

        }

        public IEnumerable<Users> GetBy(Func<Users, bool> expression)
        {
            return db.Users.Where(expression);
        }

        public void Delete(string username)
        {
            Users users = db.Users.Where(i => i.UserName.Equals(username)).FirstOrDefault();
            if (users == null)
            {
                throw new Exception("Нечего удалять");
            }
            else
            {
                db.Users.Remove(users);
                db.SaveChanges();
            }
        }
    }
}
