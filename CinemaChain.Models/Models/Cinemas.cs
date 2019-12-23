using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CinemaChain.Models.Models
{
    public class Cinemas: BaseModel
    {
        public Cinemas()
        {
            Seances = new HashSet<Seances>();
            Seats = new HashSet<Seats>();
        }
        public string CinemaName { get; set; }
        public string Address { get; set; }
        public int CountSeats { get; set; }
        public ICollection<Seances> Seances { get; set; }
        public ICollection<Seats> Seats { get; set; }
        public ICollection<CinemaImages> CinemaImages { get; set; }
    }
}
