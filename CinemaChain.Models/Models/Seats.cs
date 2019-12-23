using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CinemaChain.Models.Models
{
    public class Seats: BaseModel
    {
        public int SeatNumber { get; set; }
        public int CinemaId { get; set; }

        public virtual Cinemas Cinemas { get; set; }

    }
}
