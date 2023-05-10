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

}