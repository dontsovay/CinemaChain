using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaChain.Models.ViewModels
{
    public class SeanceAllVM
    {
        public int SeanceId { get; set; }
        public int CinemaId { get; set; }
        public string CinemaName { get; set; }
        public int FilmId { get; set; }
        public string FilmName { get; set; }
        public DateTime SeanceDate { get; set; }
        public int AllSeats { get; set; }
        public int CountSeats { get; set; }
        public int Price { get; set; }
    }
}
