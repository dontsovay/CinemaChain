using CinemaChain.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CinemaChain.Infrastructure.Interfaces
{
    public interface IOrderService
    {
        IEnumerable<Orders> GetOrders();
        Orders GetOrdersId(int entered_idorder);
        bool IsPaid(int entered_idorder);
        void DeleteOrder(int id);
        void CreateOrder(Orders orders);
        Orders UpdateOrder(int id);
        IEnumerable<Orders> PostOrders(string username);
        IEnumerable<Orders> GetSeance(int entered_idseance);
    }
}
