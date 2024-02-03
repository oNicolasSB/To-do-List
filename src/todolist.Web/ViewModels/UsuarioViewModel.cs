namespace todolist.Web.ViewModels;

public class UsuarioViewModel
{
    public int IdUsuario { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public ICollection<ListaViewModel> Listas { get; set; }
}