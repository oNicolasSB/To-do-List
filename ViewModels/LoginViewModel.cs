using System.ComponentModel.DataAnnotations;

namespace todolist.Models;

public class LoginViewModel
{
    [Display(Name = "E-mail")]
    [Required(ErrorMessage = "O Campo {0} deve ser preenchido.")]
    public string Email { get; set; }
    [Display(Name = "Senha")]
    [Required(ErrorMessage = "O Campo {0} deve ser preenchido.")]
    public string Senha { get; set; }
    [Required(ErrorMessage = "O campo {0} deve ser preenchido.")]
    [Display(Name = "Lembrar dados")]
    public bool Lembrar { get; set; }
}