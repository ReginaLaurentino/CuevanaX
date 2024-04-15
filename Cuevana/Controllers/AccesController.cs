using Cuevana.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Cuevana.Services.IServices;
using Humanizer.Localisation;

namespace Cuevana.Controllers
{
    public class AccesController : Controller
    {
        private CuevanaContext _context = new CuevanaContext();
        private readonly IUserService _userService;

        public AccesController(CuevanaContext context, IUserService userService)
        {
            _context = context;
            _userService= userService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(User _user)
        {

            UsersController uc = new UsersController(_userService);
            var Usuario = uc.Login(_user);
          
           if(Usuario ==null)
            {
                ModelState.AddModelError(string.Empty, "No se encuentra el Usuario o contraseña");
                return RedirectToAction("Index", "Acces");
               
            }
            else
            {
                 var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, Usuario.Name),
                    new Claim("username", Usuario.UserName),
                    new Claim("id", Usuario.Id.ToString()),
                    new Claim(ClaimTypes.Role, Usuario.Role),
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));


                return RedirectToAction("Index", "Home");
            }

            
        }

        public async Task<IActionResult> Salir()
        {
           
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Acces");
        }
    }
}
