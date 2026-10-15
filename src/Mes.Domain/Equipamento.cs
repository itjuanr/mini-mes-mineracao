namespace Mes.Domain;

/// <summary>
/// Estados possíveis de um equipamento de mina.
/// </summary>
public enum EstadoEquipamento
{
    Operando,
    ParadaOperacional,
    Manutencao,
    Reserva
}

/// <summary>
/// Um equipamento da frota (caminhão, escavadeira, britador...).
/// </summary>
public class Equipamento
{
    public required string Tag { get; init; }
    public required string Descricao { get; init; }
    public required string Tipo { get; init; }
    public EstadoEquipamento Estado { get; set; } = EstadoEquipamento.Reserva;
}
