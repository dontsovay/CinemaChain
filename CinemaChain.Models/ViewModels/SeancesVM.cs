using CinemaChain.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaChain.Models.ViewModels
{
    public class SeancesVM
    {
        public string seanceName { get; set; }
        public int cinemaId { get; set; }
        public int filmId { get; set; }
        public DateTime seanceDate { get; set; }
        public int price { get; set; }
    }
}
