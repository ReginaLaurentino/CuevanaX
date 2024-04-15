using Cuevana.Models;

namespace Cuevana.Services.IServices
{
    public interface IMovieService
    {
        Task<List<Movie>> GetAllMoviesAsync(int userId);
        Task<Movie> GetMovieByIdAsync(int? id);
        Task<bool> CreateMovieAsync(Movie movie, IFormFile imageFile);
        Task<bool> UpdateMovieAsync(Movie movie, IFormFile imageFile);
        Task DeleteMovieAsync(int id);
    }
}
