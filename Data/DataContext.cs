using todolist.Models;
using Microsoft.EntityFrameworkCore;

namespace todolist.Data;

public class DataContext : DbContext
{
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Lista> Listas { get; set; }
    public DbSet<Tarefa> Tarefas { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options){}
}