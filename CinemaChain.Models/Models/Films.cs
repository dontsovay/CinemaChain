using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CinemaChain.Models.Models
{
    public class Films: BaseModel
    {
        public Films()
        {
            Seances = new HashSet<Seances>();
            FilmImages = new HashSet<FilmImages>();
        }

        public string FilmName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
        public string Description { get; set; }

        public ICollection<Seances> Seances { get; set; }
        public ICollection<FilmImages> FilmImages { get; set; }

    }
}
