using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace todolist.Domain;

public class Lista
{
    [Key]
    public int IdLista { get; set; }
    [Required(ErrorMessage = "O campo {0} deve ser preenchido."), StringLength(128)]
    public string Titulo { get; set; }
    public bool Arquivada { get; set; }
    [ForeignKey("Usuario")]
    public int FkUsuario { get; set; }
    public Usuario Usuario { get; set; }
    public ICollection<Tarefa> Tarefas { get; set; }
}