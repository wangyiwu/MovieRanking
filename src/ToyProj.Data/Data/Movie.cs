using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyProj.Data.Data
{
    public class Movie
    {
        [Key]
        public int MovieId { get; set; }
        public string Title { get; set; }
        public int Budget { get; set; }
        public string Homepage { get; set; }
        public string Overview { get; set; }
        public decimal Popularity { get; set; }

        public DateTime ReleaseDate { get; set; }
        public int Revenue { get; set; }
        public int Runtime { get; set; }
        public string MovieStatus { get; set; }
        public string Tagline { get; set; }
        public decimal VotesAvg { get; set; }
        public int VotesCount { get; set; }

        public ICollection<ProductionCountry> ProductionCountries { get; set; }
        public ICollection<MovieCompany> MovieCompanies { get; set; }
        public ICollection<MovieLanguages> MovieLanguages { get; set; }
        public ICollection<MovieGenre> MovieGenres { get; set; }
        public ICollection<MovieKeywords> MovieKeywords { get; set; }
        public ICollection<MovieCast> MovieCasts { get; set; }
        public ICollection<MovieCrew> MovieCrews { get; set; }
    }
}
