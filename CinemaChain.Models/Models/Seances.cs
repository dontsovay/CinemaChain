using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CinemaChain.Models.Models
{

    public class Seances: BaseModel
    {
        public string SeanceName { get; set; }
        public int CinemaId { get; set; }
        public int FilmId { get; set; }
        public int CountSeats { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime SeanceDate { get; set; }
        public int AllSeats { get; set; }
        public int Price { get; set; }

        public virtual Cinemas Cinemas { get; set; }

        public virtual Films Films { get; set; }


        public ICollection<Orders> Orders { get; set; }
        public ICollection<BusySeats> BusySeats { get; set; }
    }
}
