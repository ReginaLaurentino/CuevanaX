using Cuevana.Models;
using Cuevana.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Cuevana.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly CuevanaContext _context;

        public MovieRepository(CuevanaContext context)
        {
            _context = context;
        }

        public async Task<List<Movie>> GetAllMoviesAsync(int userId)
        {
            return await _context.Movies
                .Include(m => m.Genre)
                .Include(m => m.User)
                .Where(m => m.UserId == userId)
                .ToListAsync();
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            return await _context.Movies
                .Include(m => m.Genre)
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task AddMovieAsync(Movie movie)
        {
            _context.Add(movie);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> MovieNameExistsAsync(string movieTitle, int id)
        {
            return await _context.Movies.AnyAsync(m => m.Title == movieTitle && m.UserId==id);
        }
        public async Task UpdateMovieAsync(Movie movie)
        {
            _context.Update(movie);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMovieAsync(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
                await _context.SaveChangesAsync();
            }
        }
    }
}
