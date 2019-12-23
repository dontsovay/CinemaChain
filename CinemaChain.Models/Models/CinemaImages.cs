using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaChain.Models.Models
{
    public class CinemaImages : BaseModel
    {
        public int CinemaId { get; set; }
        public byte[] CinemaImage { get; set; }
        public virtual Cinemas Cinemas { get; set; }
    }
}
