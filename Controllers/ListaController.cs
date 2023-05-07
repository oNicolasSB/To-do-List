
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using todolist.Data;
using todolist.Models;

namespace todolist.Controllers;

public class ListaController : Controller
{
    private readonly DataContext _db;

    public ListaController(DataContext db)
    {
        _db = db;
    }
    [Authorize]
    public IActionResult Index()
    {
        var usuario = _db.Usuarios.Find(Convert.ToInt32(User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Sid).Value));
        var listas = _db.Listas.Where(a => a.FkUsuario == usuario.IdUsuario).ToList();
        return View(listas);
    }
    [HttpGet]
    [Authorize]
    public IActionResult Create()
    {
        var lista = new Lista();
        return View(lista);
    }
    [HttpPost]
    [Authorize]
    public IActionResult Create(Lista lista)
    {
        var usuario = _db.Usuarios.Find(Convert.ToInt32(User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Sid).Value));
        lista.FkUsuario = _db.Usuarios.Find(Convert.ToInt32(User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Sid).Value)).IdUsuario;
        lista.Arquivada = false;
        ModelState.Remove("Usuario");
        if (!ModelState.IsValid)
        {
            var errors = ModelState.SelectMany(m => m.Value.Errors).Select(e => e.ErrorMessage);
            foreach (var item in errors)
            {
                System.Console.WriteLine(item);
            }
            return RedirectToAction("Index", "Home");
        }
        _db.Listas.Add(lista);
        _db.SaveChanges();
        return RedirectToAction("Index", "Lista");
    }
}