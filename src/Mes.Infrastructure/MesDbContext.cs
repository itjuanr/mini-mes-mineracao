using Mes.Domain;
using Microsoft.EntityFrameworkCore;

namespace Mes.Infrastructure;

/// <summary>
/// Ponte entre as classes do domínio e as tabelas do banco.
/// Cada DbSet vira uma tabela.
/// </summary>
public class MesDbContext(DbContextOptions<MesDbContext> options) : DbContext(options)
{
    public DbSet<Site> Sites => Set<Site>();
    public DbSet<Area> Areas => Set<Area>();
    public DbSet<ModeloEquipamento> Modelos => Set<ModeloEquipamento>();
    public DbSet<Equipamento> Equipamentos => Set<Equipamento>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Site>(site =>
        {
            site.Property(s => s.Codigo).HasMaxLength(10);
            site.Property(s => s.Nome).HasMaxLength(100);
            site.HasIndex(s => s.Codigo).IsUnique();
        });

        modelBuilder.Entity<Area>(area =>
        {
            area.Property(a => a.Codigo).HasMaxLength(10);
            area.Property(a => a.Nome).HasMaxLength(100);
            area.HasIndex(a => new { a.SiteId, a.Codigo }).IsUnique();
        });

        modelBuilder.Entity<ModeloEquipamento>(modelo =>
        {
            modelo.ToTable("ModelosEquipamento");
            modelo.Property(m => m.Nome).HasMaxLength(100);
            modelo.Property(m => m.Fabricante).HasMaxLength(100);
            modelo.Property(m => m.Tipo).HasConversion<string>().HasMaxLength(30);
        });

        modelBuilder.Entity<Equipamento>(equipamento =>
        {
            equipamento.Property(e => e.Tag).HasMaxLength(20);
            equipamento.Property(e => e.Estado).HasConversion<string>().HasMaxLength(30);
            equipamento.HasIndex(e => e.Tag).IsUnique();

            // Não deixa apagar um modelo ou área que ainda tem equipamentos.
            equipamento.HasOne(e => e.Modelo).WithMany(m => m.Equipamentos).OnDelete(DeleteBehavior.Restrict);
            equipamento.HasOne(e => e.Area).WithMany(a => a.Equipamentos).OnDelete(DeleteBehavior.Restrict);
        });
    }
}
