using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace todolist.Models;

public class Tarefa
{
    [Key]
    public int IdTarefa { get; set; }
    [Required(ErrorMessage = "O campo {0} deve ser preenchido."), StringLength(128)]
    public string Titulo { get; set; }
    [Required(ErrorMessage = "O campo {0} deve ser preenchido."), StringLength(128)]
    public string Descricao { get; set; }
    public DateTime? EntregaEstimada { get; set; }
    public DateTime? EntregaFinal { get; set; }
    public Boolean Concluido { get; set; }
    [ForeignKey("Lista")]
    public int FkLista { get; set; }
    public Lista Lista { get; set; }
}