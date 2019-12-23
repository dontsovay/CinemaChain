using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CinemaChain.Models.Models
{
    public class Orders: BaseModel
    {
        public string UserName { get; set; }

   //   public virtual Users Users { get; set; }
        public int SeanceId { get; set; }

        public virtual Seances Seances { get; set; }
        public int SeatNumber { get; set; }
        public bool IsPaid { get; set; }

    }
}
