namespace MinhaApi.Models;

public class Categoria { }
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public Icollection<Produto> Produto { get; set; } = new List<Produto>();
}