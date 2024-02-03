using todolist.Domain;
using Microsoft.EntityFrameworkCore;

namespace todolist.Infrastructure;

public class DataContext : DbContext
{
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Lista> Listas { get; set; }
    public DbSet<Tarefa> Tarefas { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options) {}
}