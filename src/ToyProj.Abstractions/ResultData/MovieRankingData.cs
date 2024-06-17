using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyProj.Abstractions.ResultData
{
    public class MovieRankingData
    {
        public string Title { get; set; }
        public string CountryCode { get; set; }
        public int ReleaseYear { get; set; }
        public string Thumbnail {  get; set; }
        public string MainCastName { get; set; }
        public string CastList { get; set; }
        public decimal VotesAvg {  get; set; }
        public int VotesCount { get; set; }
        public string GenreName { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
