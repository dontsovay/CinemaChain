using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaChain.API.AuthModel
{
    public class GlobalResult
    {
        public string UserName { get; set; }
        public string Puzzle { get; set; }
        public int Time { get; set; }
        public DateTime Date { get; set; }
        public GlobalResult(string UN, string S, int T, DateTime D)
        {
            this.UserName = UN;
            this.Puzzle = S;
            this.Time = T;
            Date = D;
        }
    }
}
