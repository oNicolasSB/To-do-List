using System.ComponentModel.DataAnnotations;

namespace todolist.Models;

public class AlterarSenhaViewModel
{
    [Required(ErrorMessage = "O campo {0} deve ser preenchido."), StringLength(128)]
    [Display(Name = "Senha Original")]
    public string SenhaOriginal { get; set; }
    [Display(Name = "Nova Senha")]
    [Required(ErrorMessage = "O campo {0} deve ser preenchido."), StringLength(32)]
    public string NovaSenha { get; set; }
    [Display(Name = "Confirmar Nova Senha")]
    [Required(ErrorMessage = "O campo {0} deve ser preenchido."), StringLength(32)]
    [Compare(nameof(NovaSenha), ErrorMessage = "As senhas devem ser iguais.")]
    public string ConfNovaSenha { get; set; }
}