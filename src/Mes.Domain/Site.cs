namespace Mes.Domain;

/// <summary>
/// Topo da hierarquia de ativos (ISA-95): um complexo minerador.
/// </summary>
public class Site
{
    public int Id { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;

    public List<Area> Areas { get; set; } = [];
}
