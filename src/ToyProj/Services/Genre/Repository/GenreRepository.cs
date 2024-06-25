using Microsoft.EntityFrameworkCore;
using ToyProj.Abstractions.ResultData;
using ToyProj.Data;

namespace ToyProj.Services.Genre.Repository
{
    public class GenreRepository: IGenreRepository
    {

        private DatabaseContext db;

        public GenreRepository(DatabaseContext db)
        {
            this.db = db;
        }

        public async Task<List<GenreData>> GetGenres()
        {
            var result = await db.Genre.Select(x => new GenreData()
            {
                GenreName = x.GenreName,
                GenreId = x.GenreId,
            }).ToListAsync();

            return result;
        }
    }
}
