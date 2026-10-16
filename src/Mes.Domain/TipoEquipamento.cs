namespace Mes.Domain;

/// <summary>
/// Famílias de equipamento. Define como o equipamento produz:
/// caminhões transportam, escavadeiras carregam, britadores processam...
/// </summary>
public enum TipoEquipamento
{
    Caminhao,
    Escavadeira,
    Carregadeira,
    Perfuratriz,
    Britador,
    TransportadorCorreia
}
