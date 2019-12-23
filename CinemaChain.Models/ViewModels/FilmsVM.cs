using CinemaChain.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaChain.Models.ViewModels
{
    public class FilmsVM
    {
        public int FilmId { get; set; }
        public string FilmName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }

        public FilmsVM(Films film)
        {
            FilmId = film.Id;
            FilmName = film.FilmName;
            StartDate = film.StartDate;
            EndDate = film.EndDate;
            Description = film.Description;
        }
    }
}
