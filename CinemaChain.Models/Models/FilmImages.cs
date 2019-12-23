using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaChain.Models.Models
{
    public class FilmImages: BaseModel
    {
        //public FilmImages(byte[] img)
        //{
        //    this.FilmImage = img;
        //}
        public int FilmId { get; set; }
        public byte[] FilmImage { get; set; }

        public virtual Films Films { get; set; }
    }
}
