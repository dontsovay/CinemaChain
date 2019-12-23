using CinemaChain.Data.Context;
using CinemaChain.Data.Interfaces.Repositories;
using CinemaChain.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CinemaChain.Data.Repositories
{
    public class OrderRepository : BaseRepository<Orders>, IOrderRepository
    {
        private DB_Context db;

        public OrderRepository(DB_Context context) : base(context)
        {
            this.db = context;
        }

        public override IEnumerable<Orders> GetAll()
        {
            return db.Set<Orders>();
        }

        public override Orders Get(int id)
        {
            return db.Set<Orders>().Find(id);
        }

        public override Orders Create(Orders order)
        {
            db.Set<Orders>().Add(order);
            db.SaveChanges();
            return order;

        }

        public override Orders Update(Orders order)
        {
            db.Entry(order).Property(c => c.IsPaid).IsModified = true;
            db.SaveChanges();
            return order;
        }

        public override void Delete(int id)
        {
            var order = db.Set<Orders>().Find(id);
            if (order == null)
            {
                throw new Exception("Нечего удалять");
            }
            else
            {
                db.Set<Orders>().Remove(order);
                db.SaveChanges();
            }
        }
        public IEnumerable<Orders> GetSeance(int seanceId)
        {
            IEnumerable<Orders> idorders = db.Orders.Where(ids => ids.SeanceId.Equals(seanceId));
            return idorders;
        }

    }
}
