using System.ComponentModel.DataAnnotations;

namespace todolist.Models;

public class Usuario
{
    [Key]
    public int IdUsuario { get; set; }
    [Required(ErrorMessage = "O campo {0} deve ser preenchido."), StringLength(128)]
    public string Nome { get; set; }
    [Required(ErrorMessage = "O campo {0} deve ser preenchido."), StringLength(128)]
    public string Email { get; set; }
    [Required(ErrorMessage = "O campo {0} deve ser preenchido."), StringLength(128)]
    public string Senha { get; set; }
    public ICollection<Lista> Listas { get; set; }
}