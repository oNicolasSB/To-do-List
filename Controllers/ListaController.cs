
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
        var mensagemErro = TempData["MensagemErro"] as string;
        if (!string.IsNullOrEmpty(mensagemErro))
        {
            ViewBag.MensagemErro = mensagemErro;
        }
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
        if (_db.Listas.Where(l => l.FkUsuario == usuario.IdUsuario).Any(l => l.Titulo == lista.Titulo))
        {
            ModelState.AddModelError("Titulo", "Você não pode cadastrar 2 listas com o mesmo título.");
            return View(lista);
        }
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
    [HttpPost]
    [Authorize]
    public IActionResult Edit(Lista lista)
    {
        var listaOriginal = _db.Listas.Find(lista.IdLista);
        if (_db.Listas.FirstOrDefault(l => l.Titulo == lista.Titulo && l.IdLista != lista.IdLista) is not null)
        {
            TempData["MensagemErro"] = "Nome de lista já cadastrado.";
            return RedirectToAction("Index", "Lista");
        }
        ModelState.Remove("Usuario");
        if (!ModelState.IsValid)
        {
            TempData["MensagemErro"] = "Insira um título válido.";
            return RedirectToAction("Index", "Lista");
        }

        listaOriginal.Titulo = lista.Titulo;
        _db.SaveChanges();
        return RedirectToAction("Index", "Lista");
    }
}