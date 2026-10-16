namespace Mes.Domain;

/// <summary>
/// Modelo de fabricante (ex.: caminhão 240 t). Vários equipamentos
/// compartilham o mesmo modelo, então tipo e capacidade ficam aqui
/// em vez de serem digitados em cada equipamento.
/// </summary>
public class ModeloEquipamento
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Fabricante { get; set; } = string.Empty;
    public TipoEquipamento Tipo { get; set; }

    /// <summary>Capacidade nominal: toneladas por ciclo (caminhão) ou t/h (britador, correia).</summary>
    public double? CapacidadeNominal { get; set; }

    public List<Equipamento> Equipamentos { get; set; } = [];
}
