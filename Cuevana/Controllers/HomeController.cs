using Cuevana.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Cuevana.Services.IServices;

namespace Cuevana.Controllers
{
   // [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMovieService _movieService;

        public HomeController(ILogger<HomeController> logger, IMovieService movieService)
        {
            _logger = logger;
            _movieService = movieService;
        }

        public async Task<IActionResult> Index()
        {
            var userIdClaim = User.FindFirst("id");
            int userId = int.Parse(userIdClaim.Value);
            var movies = await _movieService.GetAllMoviesAsync(userId);
            return View(movies);
        }

        [Authorize(Roles = "admin")]
        public IActionResult Users()
        {
            return RedirectToAction("Index", "Users");
        }
        public IActionResult Genres()
        {
            return RedirectToAction("Index", "Genres");
        }
        public IActionResult Movies()
        {
            return RedirectToAction("Index", "Movies");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpPost]
        public async Task<IActionResult> Salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Acces");
        }
    }
}
