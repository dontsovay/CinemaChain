using CinemaChain.Data.Context;
using CinemaChain.Data.Repositories;
using CinemaChain.Infrastructure.Interfaces;
using CinemaChain.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CinemaChain.Infrastructure.Services
{
    public class OwnerService: IOwnerService
    {
        UnitOfWork unitOfWork;
        //OwnersRepository ownersRepository = new OwnersRepository(new dbcontext());

        public OwnerService(DB_Context context)
        {
            unitOfWork = new UnitOfWork(context);
        }

        public IEnumerable<Owners> GetOwners()
        {
            return unitOfWork.Owners.GetAll();
        }

        //public int GetIdOwner(string enterted_username)
        //{
        //    return ownersRepository.Get
        //}
        public Owners CreateOwner(Owners owners)
        {
            Owners owner = new Owners();
            owner.FIO = owners.FIO;
            owner.Address = owners.Address;
            owner.Phone = owners.Phone;
            owner.UserName = owners.UserName;

            return unitOfWork.Owners.Create(owner);
        }
    }
}
