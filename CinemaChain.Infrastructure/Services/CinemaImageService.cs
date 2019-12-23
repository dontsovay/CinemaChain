using CinemaChain.Data.Context;
using CinemaChain.Data.Repositories;
using CinemaChain.Infrastructure.Interfaces;
using CinemaChain.Models.Models;
using CinemaChain.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CinemaChain.Infrastructure.Services
{
    public class CinemaImageService : ICinemaImageService
    {
        UnitOfWork unitOfWork;

        public CinemaImageService(DB_Context context)
        {
            unitOfWork = new UnitOfWork(context);
        }

        public IEnumerable<CinemaImages> GetCinemaImages()
        {
            return unitOfWork.CinemaImages.GetAll();
        }

        public byte[] GetCinemaImage(int filmId)
        {
            return unitOfWork.CinemaImages.GetCinemaImage(filmId);
        }

        //public IEnumerable<FilmImages> GetFilmImages(int filmId)
        //{
        //    return unitOfWork.FilmImages.GetFilmImages(filmId);
        //}
        public void DeleteCinemaImage(int id)
        {
            unitOfWork.FilmImages.Delete(id);
        }
        public CinemaImages UpdateImage(CinemaImages image)
        {
            return unitOfWork.CinemaImages.Update(image);
        }
        public CinemaImages CreateImage(CIWM cinemaim)
        {
            CinemaImages cinemaImage = new CinemaImages();
            cinemaImage.CinemaId = cinemaim.CinemaId;
            cinemaImage.CinemaImage = ConvertToByte(cinemaim.Path);

            return unitOfWork.CinemaImages.Create(cinemaImage);
        }
        public byte[] ConvertToByte(string path)
        {
            return File.ReadAllBytes(path);

        }
    }
}
