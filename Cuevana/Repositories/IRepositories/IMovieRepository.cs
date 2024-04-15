using Cuevana.Models;

namespace Cuevana.Repositories.IRepositories
{
    public interface IMovieRepository
    {
        Task<List<Movie>> GetAllMoviesAsync(int userId);
        Task<Movie> GetMovieByIdAsync(int id);
        Task AddMovieAsync(Movie movie);
        Task<bool> MovieNameExistsAsync(string movieTitle, int id);
        Task UpdateMovieAsync(Movie movie);
        Task DeleteMovieAsync(int id);
    }
}
