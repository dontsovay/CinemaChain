using CinemaChain.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaChain.Data.Repositories
{
    public class UnitOfWork
    {
        public DB_Context db;

        public UnitOfWork(DB_Context context)
        {
            db = context;
        }

        private UserRepository userRepository;
        private FilmRepository filmRepository;
        private CinemaRepository cinemaRepository;
        private AdminRepository adminRepository;
        private ClientRepository clientRepository;
        private OwnerRepository ownerRepository;
        private SeatRepository seatRepository;
        private BusySeatRepository busyseatRepository;
        private SeanceRepository seanceRepository;
        private OrderRepository orderRepository;
        private FilmImageRepository filmImageRepository;
        private CinemaImageRepository cinemaImageRepository;


        public FilmRepository Films
        {
            get
            {
                if (filmRepository == null)
                    filmRepository = new FilmRepository(db);
                return filmRepository;
            }
        }
        public FilmImageRepository FilmImages
        {
            get
            {
                if (filmImageRepository == null)
                    filmImageRepository = new FilmImageRepository(db);
                return filmImageRepository;
            }
        }

        public UserRepository Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(db);
                return userRepository;
            }
        }

        public CinemaRepository Cinemas
        {
            get
            {
                if (cinemaRepository == null)
                    cinemaRepository = new CinemaRepository(db);
                return cinemaRepository;
            }
        }

        public CinemaImageRepository CinemaImages
        {
            get
            {
                if (cinemaImageRepository == null)
                    cinemaImageRepository = new CinemaImageRepository(db);
                return cinemaImageRepository;
            }
        }

        public AdminRepository Admins
        {
            get
            {
                if (adminRepository == null)
                    adminRepository = new AdminRepository(db);
                return adminRepository;
            }
        }
        public OwnerRepository Owners
        {
            get
            {
                if (ownerRepository == null)
                    ownerRepository = new OwnerRepository(db);
                return ownerRepository;
            }
        }
        public ClientRepository Clients
        {
            get
            {
                if (clientRepository == null)
                    clientRepository = new ClientRepository(db);
                return clientRepository;
            }
        }
        public SeanceRepository Seances
        {
            get
            {
                if (seanceRepository == null)
                    seanceRepository = new SeanceRepository(db);
                return seanceRepository;
            }
        }
        public BusySeatRepository BusySeats
        {
            get
            {
                if (busyseatRepository == null)
                    busyseatRepository = new BusySeatRepository(db);
                return busyseatRepository;
            }
        }
        public SeatRepository Seats
        {
            get
            {
                if (seatRepository == null)
                    seatRepository = new SeatRepository(db);
                return seatRepository;
            }
        }
        public OrderRepository Orders
        {
            get
            {
                if (orderRepository == null)
                    orderRepository = new OrderRepository(db);
                return orderRepository;
            }
        }
    }
}
