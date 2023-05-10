using todolist.Models;
using Microsoft.EntityFrameworkCore;

namespace todolist.Data;

public class DataContext : DbContext
{
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Lista> Listas { get; set; }
    public DbSet<Tarefa> Tarefas { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.Entity<Usuario>()
        //     .HasMany(e => e.Listas)
        //     .WithOne(e => e.Usuario)
        //     .HasForeignKey(e => e.FkUsuario)
        //     .HasPrincipalKey(e => e.IdUsuario);

        // modelBuilder.Entity<Lista>()
        //     .HasMany(e => e.Tarefas)
        //     .WithOne(e => e.Lista)
        //     .HasForeignKey(e => e.FkLista)
        //     .HasPrincipalKey(e => e.IdLista);

    }


}