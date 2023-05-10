
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
        var listas = _db.Listas.Where(a => a.FkUsuario == usuario.IdUsuario && a.Arquivada == false).ToList();
        ViewBag.listasArquivadas = _db.Listas.Where(l => l.FkUsuario == usuario.IdUsuario && l.Arquivada == true).ToList();
        return View(listas);
    }
    [HttpGet]
    [Authorize]
    public IActionResult Details(int id)
    {
        var mensagemErro = TempData["MensagemErro"] as string;
        if (!string.IsNullOrEmpty(mensagemErro))
        {
            ViewBag.MensagemErro = mensagemErro;
        }
        var usuario = _db.Usuarios.Find(Convert.ToInt32(User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Sid).Value));
        var lista = _db.Listas.Find(id);
        if (lista.FkUsuario != usuario.IdUsuario)
        {
            ViewBag.mensagemErro = "Você não tem permissão para ver esta lista";
            return RedirectToAction("Index", "Home");
        }
        ViewBag.listas = _db.Listas.Where(l => l.FkUsuario == usuario.IdUsuario && l.Arquivada == false).ToList();
        ViewBag.listasArquivadas = _db.Listas.Where(l => l.FkUsuario == usuario.IdUsuario && l.Arquivada == true).ToList();
        ViewBag.tarefasConcluidas = _db.Tarefas.Where(t => t.FkLista == lista.IdLista && t.Concluido == true).ToList();
        ViewBag.tarefasPendentes = _db.Tarefas.Where(t => t.FkLista == lista.IdLista && t.Concluido == false).ToList();
        if (_db.Listas.Where(l => l.FkUsuario == usuario.IdUsuario && l.Arquivada == true).FirstOrDefault(l => l.IdLista == lista.IdLista) == null)
        {
            ViewBag.display = "d-none";
        }
        else
        {
            ViewBag.active = "active";
        }
        return View(lista);
    }
    [HttpPost]
    [Authorize]
    public IActionResult Create(Lista lista)
    {
        lista.FkUsuario = Convert.ToInt32(User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Sid).Value);
        lista.Arquivada = false;
        var usuario = _db.Usuarios.Find(Convert.ToInt32(User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Sid).Value));
        if (_db.Listas.Where(l => l.FkUsuario == usuario.IdUsuario).Any(l => l.Titulo == lista.Titulo))
        {
            ModelState.AddModelError("Titulo", "Você não pode cadastrar 2 listas com o mesmo título.");
            return View(lista);
        }

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
        var usuario = _db.Usuarios.Find(Convert.ToInt32(User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Sid).Value));
        var listaOriginal = _db.Listas.Find(lista.IdLista);
        if (listaOriginal is null)
        {
            TempData["MensagemErro"] = "Você deve editar uma lista que existe";
            return RedirectToAction("Index", "Lista");
        }
        if (_db.Listas.FirstOrDefault(l => l.Titulo == lista.Titulo && l.IdLista != lista.IdLista) is not null)
        {
            TempData["MensagemErro"] = "Nome de lista já cadastrado.";
            return RedirectToAction("Index", "Lista");
        }
        lista.Usuario = usuario;
        if (!ModelState.IsValid)
        {
            TempData["MensagemErro"] = "Insira um título válido.";
            return RedirectToAction("Index", "Lista");
        }

        listaOriginal.Titulo = lista.Titulo;
        _db.SaveChanges();
        return RedirectToAction("Index", "Lista");
    }
    [HttpPost]
    [Authorize]
    public IActionResult Delete(Lista lista)
    {
        if (_db.Tarefas.FirstOrDefault(t => t.FkLista == lista.IdLista) is not null)
        {
            TempData["MensagemErro"] = "Você não pode deletar uma lista que ainda contenha tarefas!";
            return RedirectToAction("Index", "Lista");
        }
        _db.Listas.Remove(lista);
        _db.SaveChanges();
        return RedirectToAction("Index", "Lista");
    }
    [HttpPost]
    [Authorize]
    public IActionResult Archive(Lista lista)
    {
        var listaOriginal = _db.Listas.Find(lista.IdLista);
        listaOriginal.Arquivada = true;
        _db.SaveChanges();
        return RedirectToAction("Index", "Lista");
    }
    [HttpPost]
    [Authorize]
    public IActionResult Unarchive(Lista lista)
    {
        var listaOriginal = _db.Listas.Find(lista.IdLista);
        listaOriginal.Arquivada = false;
        _db.SaveChanges();
        return RedirectToAction("Index", "Lista");
    }
}