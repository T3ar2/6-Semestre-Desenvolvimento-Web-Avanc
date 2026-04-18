namespace MinhaApi.Models;

public class Categoria { }
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public decimal Preco { get; set; }
    public Icollection<Categoria> Categorias { get; set; } = new List<Categorias>();
}