namespace Mes.Domain;

/// <summary>
/// Área dentro de um site (ex.: Mina, Usina de Beneficiamento).
/// </summary>
public class Area
{
    public int Id { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;

    public int SiteId { get; set; }
    public Site Site { get; set; } = null!;

    public List<Equipamento> Equipamentos { get; set; } = [];
}
