using Cuevana.Models;
using Cuevana.Repositories.IRepositories;
using Cuevana.Services.IServices;

namespace Cuevana.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<IEnumerable<Genre>> GetAllGenresAsync(int userId)
        {
            return await _genreRepository.GetAllGenresAsync(userId);
        }

        public async Task<Genre> GetGenreByIdAsync(int id)
        {
            return await _genreRepository.GetGenreByIdAsync(id);
        }

        public async Task<bool> GenreNameExistsAsync(string genreName, int id)
        {
            return await _genreRepository.GenreNameExistsAsync(genreName, id);
        }

        public async Task AddGenreAsync(Genre genre)
        {
            await _genreRepository.AddGenreAsync(genre);
        }

        public async Task<bool> GenreExistsAsync(int id)
        {
            return await _genreRepository.GenreExistsAsync(id);
        }

        public async Task UpdateGenreAsync(Genre genre)
        {
            await _genreRepository.UpdateGenreAsync(genre);
        }

        public async Task DeleteGenreAsync(int id)
        {
            try
            {
                await _genreRepository.DeleteGenreAsync(id);
            }
            catch (DatabaseOperationException ex)
            {
                throw new ServiceException("Error al eliminar el género.", ex);
            }
            
        }
    }
}
