namespace MinhaApi.Models;

public class Usuario 
{ 
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<Produto> Produtos { get; set; } = new List<Produto>();
}