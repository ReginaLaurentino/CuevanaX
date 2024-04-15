using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cuevana.Models;
using Cuevana.Services.IServices;
using Humanizer.Localisation;

namespace Cuevana.Controllers
{
    public class GenresController : Controller
    {
        private readonly IGenreService _genreService;
        private readonly IUserService _userService;

        public GenresController(IGenreService genreService, IUserService userService)
        {
            _genreService = genreService;
            _userService = userService; 
        }
        // GET: Genres
        public async Task<IActionResult> Index()
        {
            var userIdClaim = User.FindFirst("id");
            var genres = await _genreService.GetAllGenresAsync(int.Parse(userIdClaim.Value));
            return View(genres);
        }

        // GET: Genres/Create
        public async Task<IActionResult> Create()
        {
            var userIdClaim = User.FindFirst("id");
            var userId = int.Parse(userIdClaim.Value);
            var users = await _userService.GetAllUsersAsync(); 
            ViewData["UserId"] = new SelectList(users, "Id", "Id", userId);
            return View();
        }

        // POST: Genres/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GenreName,UserId")] Genre genre)
        {
            try
            {
                var userIdClaim = User.FindFirst("id");
                genre.UserId = int.Parse(userIdClaim.Value);
                if (ModelState.IsValid)
                {
                    if (!await _genreService.GenreNameExistsAsync(genre.GenreName, genre.UserId))
                    {
                        await _genreService.AddGenreAsync(genre);
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "El Género ya existe");
                    }
                }
                var users = await _userService.GetAllUsersAsync(); 
                ViewData["UserId"] = new SelectList(users, "Id", "Id", genre.UserId);
                return View(genre);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET: Genres/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var genre = await _genreService.GetGenreByIdAsync(id.Value);
            if (genre == null)
            {
                return NotFound();
            }
            var userIdClaim = User.FindFirst("id");
            var userId = int.Parse(userIdClaim.Value);
            var users = await _userService.GetAllUsersAsync();
            ViewData["UserId"] = new SelectList(users, "Id", "Id", genre.UserId);
            return View(genre);
        }

        // POST: Genres/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GenreName,UserId")] Genre genre)
        {
            if (id != genre.Id)
            {
                return NotFound();
            }
            var nameexist = await _genreService.GenreNameExistsAsync(genre.GenreName, genre.UserId);
            if (nameexist)
            {
                ModelState.AddModelError(string.Empty, "El Género ya existe");
                return RedirectToAction(nameof(Index));
            }
            if (ModelState.IsValid)
            {
                try
                {
                    await _genreService.UpdateGenreAsync(genre);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _genreService.GenreExistsAsync(genre.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            var userIdClaim = User.FindFirst("id");
            var userId = int.Parse(userIdClaim.Value);
            var users = await _userService.GetAllUsersAsync();
            ViewData["UserId"] = new SelectList(users, "Id", "Id", genre.UserId);
            return View(genre);
        }

        // GET: Genres/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            
           
                if (id == null)
                {
                    return NotFound();
                }

                 var genre = await _genreService.GetGenreByIdAsync(id.Value);
                if (genre == null)
                {
                    return NotFound();
                }

                return View(genre);
         
           

        }

        // POST: Genres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                if (await _genreService.GenreExistsAsync(id))
                {
                    await _genreService.DeleteGenreAsync(id);
                }

                return RedirectToAction(nameof(Index));
            }
            catch (ServiceException ex)
            {
                ModelState.AddModelError("", "No se pudo Eliminar el Genero");
                return View();
            }
           
        }
    
    }
}
