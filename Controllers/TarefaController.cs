using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using todolist.Data;
using todolist.Models;

namespace todolist.Controllers;

public class TarefaController : Controller
{
    private readonly DataContext _db;

    public TarefaController(DataContext db)
    {
        _db = db;
    }
    [HttpPost]
    [Authorize]
    public IActionResult Create(Tarefa tarefa)
    {
        tarefa.Concluido = false;
        var usuario = _db.Usuarios.Find(Convert.ToInt32(User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Sid).Value));
        if (_db.Tarefas.Where(t => t.FkLista == tarefa.FkLista).Any(t => t.Titulo == tarefa.Titulo))
        {
            ViewBag.mensagemErro = "Titulo já cadastrado nesta lista.";
            return RedirectToAction("Details", "Lista", tarefa.FkLista);
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
        _db.Tarefas.Add(tarefa);
        _db.SaveChanges();
        return RedirectToAction("Details", "Lista", new { id = tarefa.FkLista });
    }
    [HttpPost]
    [Authorize]
    public IActionResult Edit(Tarefa tarefa)
    {
        var usuario = _db.Usuarios.Find(Convert.ToInt32(User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Sid).Value));
        var tarefaOriginal = _db.Tarefas.Find(tarefa.IdTarefa);
        if(tarefaOriginal is null) {
            TempData["MensagemErro"] = "Você deve editar uma tarefa que existe";
            return RedirectToAction("Index", "Lista");
        }
        if (_db.Tarefas.FirstOrDefault(l => l.Titulo == tarefa.Titulo && l.IdTarefa != tarefa.IdTarefa) is not null)
        {
            ViewBag.mensagemErro = "Titulo já cadastrado nesta lista.";
            return RedirectToAction("Details", "Lista", tarefa.FkLista);
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
        tarefaOriginal.Titulo = tarefa.Titulo;
        tarefaOriginal.Descricao = tarefa.Descricao;
        tarefaOriginal.EntregaEstimada = tarefa.EntregaEstimada;
        _db.SaveChanges();
        return RedirectToAction("Details", "Lista", new { id = tarefaOriginal.FkLista });
    }
    [HttpPost]
    [Authorize]
    public IActionResult Delete(Tarefa tarefa)
    {
        var tarefaOriginal = _db.Tarefas.Find(tarefa.IdTarefa);
        _db.Tarefas.Remove(tarefaOriginal);
        _db.SaveChanges();
        return RedirectToAction("Details", "Lista",  new { id = tarefaOriginal.FkLista });
    }
    [HttpPost]
    [Authorize]
    public IActionResult Complete(Tarefa tarefa)
    {
        var tarefaOriginal = _db.Tarefas.Find(tarefa.IdTarefa);
        tarefaOriginal.Concluido = true;
        tarefaOriginal.EntregaFinal = DateTime.Now;
        _db.SaveChanges();
        return RedirectToAction("Details", "Lista",  new { id = tarefaOriginal.FkLista });
    }
    [HttpPost]
    [Authorize]
    public IActionResult Uncomplete(Tarefa tarefa)
    {
        var tarefaOriginal = _db.Tarefas.Find(tarefa.IdTarefa);
        tarefaOriginal.Concluido = false;
        tarefaOriginal.EntregaFinal = null;
        _db.SaveChanges();
        return RedirectToAction("Details", "Lista",  new { id = tarefaOriginal.FkLista });
    }

}