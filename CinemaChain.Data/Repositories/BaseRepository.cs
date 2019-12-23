using CinemaChain.Data.Context;
using CinemaChain.Data.Interfaces.Repositories;
using CinemaChain.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaChain.Data.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : BaseModel
    {
        private DB_Context _context;


        protected BaseRepository(DB_Context context)
        {
            _context = context;
        }

        public virtual T Create(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChangesAsync();
            return entity;
        }

        public  virtual T Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChangesAsync();
            return entity;
        }

        public virtual T Update(string username, T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChangesAsync();
            return entity;
        }
        public virtual void Delete(int id)
        {
            var entity = _context.Set<T>().Find(id);
            _context.Set<T>().Remove(entity);
           _context.SaveChangesAsync();
        }
        public virtual void Delete(string username)
        {
            var entity = _context.Set<T>().Find(username);
            _context.Set<T>().Remove(entity);
            _context.SaveChangesAsync();
        }

        public virtual T Get(int id)
        {
            return _context.Set<T>().Find(id);
        }
        public virtual T Get(string username)
        {
            return _context.Set<T>().Find(username);
        }
        public virtual IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

    }
}
