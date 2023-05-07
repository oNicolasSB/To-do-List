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

    [Authorize]
    public IActionResult Index()
    {
        var usuario = _db.Usuarios.Find(Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value));
        return View(usuario);
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
                new Claim(ClaimTypes.Sid, usuario.IdUsuario.ToString()),
                new Claim(ClaimTypes.Role, "usuario")
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
            ModelState.AddModelError("Senha", "Senha incorreta.");
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

    [HttpGet]
    [Authorize]
    public IActionResult Edit()
    {
        var usuario = _db.Usuarios.Find(Convert.ToInt32(User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Sid).Value));
        if (usuario is null)
        {
            return RedirectToAction("Index", "Home");
        }
        return View(usuario);
    }
    [HttpPost]
    [Authorize]
    public IActionResult Edit(Usuario usuario)
    {
        if(_db.Usuarios.Find(Convert.ToInt32(User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Sid).Value)).IdUsuario != usuario.IdUsuario)
        {
            return RedirectToAction("Index", "Home");
        }
        var usuariooriginal = _db.Usuarios.Find(usuario.IdUsuario);
        if (usuariooriginal is null)
            return RedirectToAction("Index", "Home");
        ModelState.Remove("Senha");
        ModelState.Remove("IdUsuario");
        if (!ModelState.IsValid)
        {
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine(error.ErrorMessage);
            }
            return RedirectToAction("Index", "Home");
        }
        if (_db.Usuarios.FirstOrDefault(u => u.Email == usuario.Email && u.IdUsuario != usuariooriginal.IdUsuario) is not null)
        {
            ModelState.AddModelError("Email", "E-mail em uso");
            return View(usuario);
        }


        usuariooriginal.Nome = usuario.Nome;
        usuariooriginal.Email = usuario.Email;
        _db.SaveChanges();
        return RedirectToAction("Index", "Usuario");
    }
    [HttpGet]
    [Authorize]
    public IActionResult AltSenha()
    {
        var alterarSenha = new AlterarSenhaViewModel();
        return View(alterarSenha);
    }
    [HttpPost]
    [Authorize]
    public IActionResult AltSenha(AlterarSenhaViewModel altSenha)
    {
        if(!ModelState.IsValid) return View(altSenha);
        var usuario = _db.Usuarios.Find(Convert.ToInt32(User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Sid).Value));
        if(usuario is null) return RedirectToAction("Index", "Home");
        usuario.Senha = HashPassword(altSenha.NovaSenha, 10);
        _db.SaveChanges();
        return RedirectToAction("Index", "Usuario");
    }
}