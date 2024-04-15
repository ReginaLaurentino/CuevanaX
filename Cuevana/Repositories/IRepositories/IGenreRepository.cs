using Cuevana.Models;

namespace Cuevana.Repositories.IRepositories
{
    public interface IGenreRepository
    {
        Task<IEnumerable<Genre>> GetAllGenresAsync(int userId);
        Task<Genre> GetGenreByIdAsync(int id);
        Task<bool> GenreNameExistsAsync(string genreName, int id);
        Task AddGenreAsync(Genre genre);
        Task<bool> GenreExistsAsync(int id);
        Task UpdateGenreAsync(Genre genre);
        Task DeleteGenreAsync(int id);
    }
}
