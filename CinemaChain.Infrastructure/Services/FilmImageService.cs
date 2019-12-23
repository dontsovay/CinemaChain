using CinemaChain.Data.Context;
using CinemaChain.Data.Repositories;
using CinemaChain.Infrastructure.Interfaces;
using CinemaChain.Models.Models;
using CinemaChain.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CinemaChain.Infrastructure.Services
{
    public class FilmImageService : IFilmImageService
    {
        UnitOfWork unitOfWork;

        public FilmImageService(DB_Context context)
        {
            unitOfWork = new UnitOfWork(context);
        }


        public IEnumerable<FilmImages> GetFilmImages()
        {
            return unitOfWork.FilmImages.GetAll();
        }

        public byte[] GetFilmImage(int filmId)
        {
            return unitOfWork.FilmImages.GetFilmImage(filmId);
        }

        //public IEnumerable<FilmImages> GetFilmImages(int filmId)
        //{
        //    return unitOfWork.FilmImages.GetFilmImages(filmId);
        //}
        public void DeleteFilmImage(int id)
        {
            unitOfWork.FilmImages.Delete(id);
        }
        public FilmImages UpdateImage(FilmImages image)
        {
            return unitOfWork.FilmImages.Update(image);
        }
        public FilmImages CreateImage(FIWM filmim)
        {
            FilmImages filmImage = new FilmImages();
            filmImage.FilmId = filmim.FilmId;
            filmImage.FilmImage = ConvertToByte(filmim.Path);

            return unitOfWork.FilmImages.Create(filmImage);
        }
        public byte[] ConvertToByte(string path)
        {
            return File.ReadAllBytes(path);
          
        }
    }
}
