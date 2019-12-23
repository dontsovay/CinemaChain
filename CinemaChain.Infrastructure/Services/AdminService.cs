using CinemaChain.Data.Context;
using CinemaChain.Data.Repositories;
using CinemaChain.Infrastructure.Interfaces;
using CinemaChain.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace CinemaChain.Infrastructure.Services
{
    public class AdminService: IAdminService
    {
        UnitOfWork unitOfWork;

        public AdminService(DB_Context context)
        {
            unitOfWork = new UnitOfWork(context);
        }

        public IEnumerable<Admins> getAdmins()
        {
            return unitOfWork.Admins.GetAll();
        }
        public Admins getAdminId(string entered_username)
        {
            return unitOfWork.Admins.GetAll().Where(f => f.Users.UserName.Equals(entered_username)).FirstOrDefault();
        }
        public string getFIO(string entered_username)
        {
            var data = unitOfWork.Admins.GetAll().Where(nf => nf.Users.UserName.Equals(entered_username)).FirstOrDefault();
            return data.FIO;

        }

        public void deleteAdmin(int id)
        {
            unitOfWork.Admins.Delete(id);
        }

        public Admins GetAdmin(string username)
        {
            return unitOfWork.Admins.GetAdmin(username);
        }

        public Admins CreateAdmin(Admins admins)
        {
            Admins admin = new Admins();
            admin.FIO = admins.FIO;
            admin.Address = admins.Address;
            admin.Phone = admins.Phone;
            admin.UserName = admin.UserName;

            return unitOfWork.Admins.Create(admin);
            
        }
    }
}
