using CinemaChain.Models.Models;
using CinemaChain.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaChain.Infrastructure.Interfaces
{
    public interface IFilmImageService
    {
        public IEnumerable<FilmImages> GetFilmImages();
        public byte[] GetFilmImage(int filmid);
        //public IEnumerable<FilmImages> GetFilmImages(int filmId);
        public void DeleteFilmImage(int id);
        public FilmImages UpdateImage(FilmImages image);
        public FilmImages CreateImage(FIWM fIWM);
        public byte[] ConvertToByte(string path);
    }
}
