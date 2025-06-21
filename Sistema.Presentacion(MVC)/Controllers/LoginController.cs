using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Generators;
using Sistema.API.Consume;
using SistemaGFYMP.Modelos;
using System.Security.Claims;

namespace Sistema.Presentacion_MVC_.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        // POST: UsuariosController/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password)
        {
            var usuario = CRUD<Usuario>.GetAll(); // Obtienes todos los usuarios
            var user = usuario.FirstOrDefault(u => u.Email == email); // Buscas si existe un usuario con ese correo

            if (user == null) // Si el correo no está registrado
            {
                ModelState.AddModelError("", "Correo incorrecto.");
                return RedirectToAction("Index", "Login");// Retorna la vista con el mensaje de error
            }

            // Si el correo existe, verificamos la contraseña
            if (user.Contrasena == password)
            {
                //// Si la contraseña es correcta, autenticamos al usuario
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Nombre),
                    new Claim(ClaimTypes.Email, user.Email)
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                // Iniciar sesión con la cookie de autenticación
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                return RedirectToAction("Index", "Home"); // Redirige a la página principal si las credenciales son correctas
            }
            else
            {
                ModelState.AddModelError("", "Contraseña incorrecta.");
                return View(); // Retorna la vista con el mensaje de error si la contraseña es incorrecta
            }
        }

        // Acción para cerrar sesión
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home"); // Redirige a la página de login
        }

        // GET: UsuariosController/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: UsuariosController/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(string nombre, string email, string password)
        {
            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "Todos los campos son obligatorios.");
                return View();
            }
            var nuevoUsuario = new Usuario
            {
                Nombre = nombre,
                Email = email,
                Contrasena = BCrypt.Net.BCrypt.HashPassword(password) // Hasheamos la contraseña antes de guardarla
            };
            CRUD<Usuario>.Create(nuevoUsuario);
            return RedirectToAction("Index", "Login");
        }
    }
}
