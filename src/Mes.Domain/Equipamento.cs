namespace Mes.Domain;

/// <summary>
/// Um equipamento físico da frota, identificado pela sua tag (ex.: CM-101).
/// O tipo e a capacidade vêm do modelo; a localização vem da área.
/// </summary>
public class Equipamento
{
    public int Id { get; set; }
    public string Tag { get; set; } = string.Empty;
    public EstadoEquipamento Estado { get; set; } = EstadoEquipamento.Reserva;
    public bool Ativo { get; set; } = true;

    public int ModeloId { get; set; }
    public ModeloEquipamento Modelo { get; set; } = null!;

    public int AreaId { get; set; }
    public Area Area { get; set; } = null!;
}
