using CinemaChain.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaChain.Models.ViewModels
{
    public class CinemasVM
    {
        public int CinemaId { get; set; }
        public string CinemaName { get; set; }
        public string Address { get; set; }
        public int CountSeats { get; set; }

        public CinemasVM(Cinemas cinemas)
        {
            CinemaId = cinemas.Id;
            CinemaName = cinemas.CinemaName;
            Address = cinemas.Address;
            CountSeats = cinemas.CountSeats;
        }
    }
}
