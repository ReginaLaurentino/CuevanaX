using Cuevana.Models;
using Cuevana.Repositories.IRepositories;
using Cuevana.Services.IServices;

namespace Cuevana.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MovieService(IMovieRepository movieRepository, IWebHostEnvironment webHostEnvironment)
        {
            _movieRepository = movieRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public Task<List<Movie>> GetAllMoviesAsync(int userId)
        {
            return _movieRepository.GetAllMoviesAsync(userId);
        }

        public Task<Movie> GetMovieByIdAsync(int? id)
        {
            return _movieRepository.GetMovieByIdAsync((int)id);
        }
        public async Task<bool> CreateMovieAsync(Movie movie, IFormFile imageFile)
        {
            try
            {
                if (await _movieRepository.MovieNameExistsAsync(movie.Title, (int)movie.UserId))
                {
                    return false; 
                }

                string uniqueFileName = await GuardarArchivoAsync(imageFile, movie.Title);
                if (uniqueFileName != null)
                {
                    movie.Movieimage = uniqueFileName;
                }

                await _movieRepository.AddMovieAsync(movie);
                return true;
            }
            catch (Exception ex)
            {
               
                throw ex;
            }
        }

        public async Task<bool> UpdateMovieAsync(Movie movie, IFormFile imageFile)
        {
            try
            {
                if (!movie.Movieimage.Equals(imageFile.Name))
                {
                    string nombreArchivo = await GuardarArchivoAsync(imageFile, movie.Title);
                    if (nombreArchivo != null)
                    {
                        movie.Movieimage = nombreArchivo;
                    }
                }

                await _movieRepository.UpdateMovieAsync(movie);
                return true;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public async Task DeleteMovieAsync(int id)
        {
            await _movieRepository.DeleteMovieAsync(id);
        }

        private async Task<string> GuardarArchivoAsync(IFormFile archivo, string titulo)
        {
            if (archivo != null && archivo.Length > 0)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }
                string extension = Path.GetExtension(archivo.FileName);
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + titulo + extension;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await archivo.CopyToAsync(stream);
                }
                return uniqueFileName;
            }
            return null;
        }

    }
}
