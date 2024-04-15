using Cuevana.Models;
using Cuevana.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Cuevana.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly CuevanaContext _context;

        public GenreRepository(CuevanaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Genre>> GetAllGenresAsync(int userId)
        {
            return await _context.Genres.Include(g => g.User).Where(g => g.UserId == userId).ToListAsync();
        }

        public async Task<Genre> GetGenreByIdAsync(int id)
        {
            return await _context.Genres.Include(g => g.User).FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task<bool> GenreNameExistsAsync(string genreName, int id)
        {
            return await _context.Genres.AnyAsync(g => g.GenreName == genreName && g.UserId==id);
        }

        public async Task AddGenreAsync(Genre genre)
        {
            _context.Add(genre);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> GenreExistsAsync(int id)
        {
            return await _context.Genres.AnyAsync(g => g.Id == id);
        }

        public async Task UpdateGenreAsync(Genre genre)
        {
            _context.Update(genre);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteGenreAsync(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            try
            {
                if (genre != null)
                {
                    _context.Genres.Remove(genre);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new DatabaseOperationException("Error al eliminar el género.", ex);
            }

        }
    }

}
