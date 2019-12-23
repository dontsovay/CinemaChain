using CinemaChain.Data.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaChain.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {

        IAdminRepository Admins { get; }
        IClientRepository Clients { get; }
        IOwnerRepository Owners { get; }
        IUserRepository Users { get; }
        ICinemaRepository Cinemas { get; }
        IFilmRepository Films { get; }
        ISeanceRepository Seances { get; }
        IOrderRepository Orders { get; }
        ISeatRepository Seats { get; }
        IBusySeatRepository BusySeats { get; }
        IFilmImageRepository FilmImages { get; }
        void Save();
    }
}
