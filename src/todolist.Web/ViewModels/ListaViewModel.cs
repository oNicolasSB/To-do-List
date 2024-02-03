namespace todolist.Web.ViewModels;

public class ListaViewModel
{
    public int IdLista { get; set; }
    public string Titulo { get; set; }
    public bool Arquivada { get; set; }
    public int FkUsuario { get; set; }
    public UsuarioViewModel Usuario { get; set; }
    public ICollection<TarefaViewModel> Tarefas { get; set; }
}