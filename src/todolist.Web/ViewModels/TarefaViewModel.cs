namespace todolist.Web.ViewModels;

public class TarefaViewModel
{
    public int IdTarefa { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public DateTime? EntregaEstimada { get; set; }
    public DateTime? EntregaFinal { get; set; }
    public Boolean Concluido { get; set; }
    public int FkLista { get; set; }
    public ListaViewModel Lista { get; set; }
}