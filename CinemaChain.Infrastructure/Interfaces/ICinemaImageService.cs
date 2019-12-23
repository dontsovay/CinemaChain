using CinemaChain.Models.Models;
using CinemaChain.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaChain.Infrastructure.Interfaces
{
    public interface ICinemaImageService
    {
        public IEnumerable<CinemaImages> GetCinemaImages();
        public byte[] GetCinemaImage(int cinemaid);
        public void DeleteCinemaImage(int id);
        public CinemaImages UpdateImage(CinemaImages image);
        public CinemaImages CreateImage(CIWM fIWM);
        public byte[] ConvertToByte(string path);
    }
}
