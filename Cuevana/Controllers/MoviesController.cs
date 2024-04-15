using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cuevana.Models;

using System.IO;
using Microsoft.AspNetCore.Hosting;
using Humanizer.Localisation;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Cuevana.Services.IServices;
using Cuevana.Services;

namespace Cuevana.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly IUserService _userService;
        private readonly IGenreService _genreService;

        public MoviesController(IMovieService movieService, IUserService userService, IGenreService genreService)
        {
            _movieService = movieService;
            _userService = userService;
            _genreService = genreService;
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            var userIdClaim = User.FindFirst("id");
            int userId = int.Parse(userIdClaim.Value);
            var movies = await _movieService.GetAllMoviesAsync(userId);
            return View(movies);
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var movie = await _movieService.GetMovieByIdAsync(id.Value);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // GET: Movies/Create
        public async Task<IActionResult> Create()
        {
            var userIdClaim = User.FindFirst("id");
            int userId = int.Parse(userIdClaim.Value);
            var users = await _userService.GetAllUsersAsync(); 
            var Genres = await _genreService.GetAllGenresAsync(userId);
            ViewData["UserId"] = new SelectList(users, "Id", "Id", userId);
            ViewData["GenreId"] = new SelectList(Genres, "Id", "GenreName", userId);
           // ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Movies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,ReleaseYear,Movieimage,Casting,Director,GenreId,Link,UserId")] Movie movie, IFormFile ImageFile)
        {
            try
            {
                var userIdClaim = User.FindFirst("id");
                int userId = int.Parse(userIdClaim.Value);
                movie.UserId = userId;
                movie.ReleaseYear= DateTime.Now;
                movie.Movieimage = ImageFile.FileName;
                if (ModelState.IsValid)
                {
                    if (await _movieService.CreateMovieAsync(movie, ImageFile))
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe una Película con ese mismo Nombre.");
                    }
                }
                
                var users = await _userService.GetAllUsersAsync(); 
                var Genres = await _genreService.GetAllGenresAsync(userId);
                ViewData["UserId"] = new SelectList(users, "Id", "Id", userId);
                ViewData["GenreId"] = new SelectList(Genres, "Id", "GenreName", userId);
                return View(movie);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(movie);
            }
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var movie = await _movieService.GetMovieByIdAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            var userIdClaim = User.FindFirst("id");
            int userId = int.Parse(userIdClaim.Value);
            var users = await _userService.GetAllUsersAsync(); 
            var Genres = await _genreService.GetAllGenresAsync(userId);
            ViewData["UserId"] = new SelectList(users, "Id", "Id", userId);
            ViewData["GenreId"] = new SelectList(Genres, "Id", "GenreName", userId);
            return View(movie);
        }

        // POST: Movies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseYear,Movieimage,Casting,Director,GenreId,Link,UserId")] Movie movie, IFormFile ImageFile)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }
            var userIdClaim = User.FindFirst("id");
            int userId = int.Parse(userIdClaim.Value);
            movie.UserId = userId;  

            if (await _movieService.UpdateMovieAsync(movie, ImageFile))
            {
                return RedirectToAction(nameof(Index));
            }

            
            var users = await _userService.GetAllUsersAsync(); 
            var Genres = await _genreService.GetAllGenresAsync(userId);
            ViewData["UserId"] = new SelectList(users, "Id", "Id", userId);
            ViewData["GenreId"] = new SelectList(Genres, "Id", "GenreName", userId);
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var movie = await _movieService.GetMovieByIdAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _movieService.DeleteMovieAsync(id);
            return RedirectToAction(nameof(Index));
        }
      
    }
}
