using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
	public DbSet<Produto> Produtos { get; set; }
	public DbSet<Categoria> Categoria { get; set; }
	public DbSet<Usuario> Usuario { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Produto>()
			.HasOne(p => p.Categoria)
			.WithMany(c => c.Produtos)
			.HasForeignKey(p => p.CategoriaId);

		modelBuilder.Entity<Produto>(entity => {
			entity.Property(p => p.Preco).HasPrecision(18, 2).IsRequired();
			entity.Property(p => p.Nome).HasMaxLength(120).IsRequired();
		});

		modelBuilder.Entity<Categoria>(entity =>
		{
			entity.Property(c => c.Nome).HasMaxLength(80).IsRequired();
		};

		modelBuilder.Entity<Usuario>().HasIndex(u => u.Login).IsUnique();
	}
}