using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using todolist.Data;
using todolist.Models;
using static BCrypt.Net.BCrypt;

namespace todolist.Controllers;

public class UsuarioController : Controller
{
    private readonly DataContext _db;

    public UsuarioController(DataContext db)
    {
        _db = db;
    }

    [HttpGet]
    public IActionResult Create()
    {
        var usuario = new CadastroViewModel();
        return View(usuario);
    }
    [HttpPost]
    public IActionResult Create(CadastroViewModel usuarioViewModel)
    {
        if (!ModelState.IsValid) return View(usuarioViewModel);
        if (_db.Usuarios.Any(u => u.Email == usuarioViewModel.Email))
        {
            ModelState.AddModelError("Email", "Este e-mail já foi cadastrado");
            return View(usuarioViewModel);
        }
        var usuario = new Usuario();
        usuario.Nome = usuarioViewModel.Nome;
        usuario.Email = usuarioViewModel.Email;
        usuario.Senha = HashPassword(usuarioViewModel.Senha, 10);
        _db.Usuarios.Add(usuario);
        _db.SaveChanges();
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult Login()
    {
        var login = new LoginViewModel();
        return View(login);
    }
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel login)
    {
        if (!ModelState.IsValid) return View(login);
        var usuario = _db.Usuarios.FirstOrDefault(u => u.Email == login.Email);
        if (usuario is null)
        {
            ModelState.AddModelError("Email", "E-mail não encontrado.");
            return View(login);
        }
        var verificado = Verify(login.Senha, usuario.Senha);
        if (verificado)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim(ClaimTypes.Sid, usuario.IdUsuario.ToString())
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                IsPersistent = login.Lembrar,
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
            System.Console.WriteLine($"O Usuário {usuario.Email} logou às {DateTime.Now}");

            return RedirectToAction("Index", "Home");
        }
        else
        {
            return View(login);
        }
    }
    public async Task<IActionResult> Logout(string returnUrl = null)
    {
        await HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);

        if (returnUrl is null)
        {
            return RedirectToAction("Index", "Home");
        }

        return Redirect(returnUrl);
    }
}