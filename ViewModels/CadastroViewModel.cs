using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace todolist.Models;

public class CadastroViewModel
{
    [Required(ErrorMessage = "O campo {0} deve ser preenchido."), StringLength(128)]
    public string Nome { get; set; }
    [Display(Name = "E-mail")]
    [Required(ErrorMessage = "O campo {0} deve ser preenchido."), StringLength(128)]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo {0} deve ser preenchido."), StringLength(32)]
    public string Senha { get; set; }
    [Display(Name = "Confirmar Nova Senha")]
    [Required(ErrorMessage = "O campo {0} deve ser preenchido."), StringLength(32)]
    [Compare(nameof(Senha), ErrorMessage = "As senhas devem ser iguais.")]
    public string ConfSenha { get; set; }
}