using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CinemaChain.Models.Models
{
    public class BusySeats: BaseModel
    {
        public int SeatNumber { get; set; }
        public int SeanceId { get; set; }

        public virtual Seances Seances { get; set; }
        public bool IsBusy { get; set; }

    }
}
