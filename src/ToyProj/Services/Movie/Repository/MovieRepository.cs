using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToyProj.Abstractions.ResultData;
using ToyProj.Data;
using ToyProj.Services.Movie.Models;

namespace ToyProj.Services.Movie.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private DatabaseContext db;

        public MovieRepository(DatabaseContext db)
        {
            this.db = db;
        }

        public async Task<List<MovieRankingData>> GetMovieRankings(MovieRankingRequestModel query)
        {
            string orderBy = string.IsNullOrEmpty(query.OrderBy) ? "ReleaseYear" : query.OrderBy;

            string queryByGenere = "";

            if (!string.IsNullOrEmpty(query.GenreName))
            {
                queryByGenere = $@"where g.GenreName = '{query.GenreName}'";
            }

            int count = (query.Count.HasValue && query.Count > 0)  ? query.Count.Value : 100;

            var baseQueryString = $@"SELECT top ({count}) m.Title as Title, c.CountryIsoCode as CountryCode, Year(m.ReleaseDate) as ReleaseYear, m.Thumbnail, p.PersonName as CastName, mc.CastOrder as CastOrder, m.VotesAvg, m.VotesCount, g.GenreName, 
                                m.ReleaseDate as ReleaseDate
                                  FROM Movie m 
                                  join ProductionCountry pc on m.MovieId = pc.MovieId
                                  join Country c on pc.CountryId = c.CountryId
                                  join MovieCast mc on mc.MovieId = m.MovieId
                                  join Person p on p.PersonId = mc.PersonId
                                  join MovieGenre mg on mg.MovieId = m.MovieId
                                  join Genre g on mg.GenreId = g.GenreId 
                                  {queryByGenere}
                                  order by {orderBy} desc
                                ";

            var result = await db.Database.SqlQueryRaw<MovieRankingData>(baseQueryString).ToListAsync();

            return result;

        }
    }


}
