using ToyProj.Abstractions.ResultData;

namespace ToyProj.Services.Genre.Repository
{
    public interface IGenreRepository
    {
        Task<List<GenreData>> GetGenre();
    }
}
