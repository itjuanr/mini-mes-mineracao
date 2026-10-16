using Mes.Domain;
using MudBlazor;

namespace Mes.Web.Components;

/// <summary>
/// Cores e ícones usados em todas as telas, para manter o visual consistente.
/// </summary>
public static class Visual
{
    public static Color CorDoEstado(EstadoEquipamento estado) => estado switch
    {
        EstadoEquipamento.Operando => Color.Success,
        EstadoEquipamento.ParadaOperacional => Color.Warning,
        EstadoEquipamento.Manutencao => Color.Error,
        _ => Color.Default
    };

    public static string IconeDoTipo(TipoEquipamento tipo) => tipo switch
    {
        TipoEquipamento.Caminhao => Icons.Material.Filled.LocalShipping,
        TipoEquipamento.Escavadeira => Icons.Material.Filled.Agriculture,
        TipoEquipamento.Carregadeira => Icons.Material.Filled.Agriculture,
        TipoEquipamento.Perfuratriz => Icons.Material.Filled.Hardware,
        TipoEquipamento.Britador => Icons.Material.Filled.Compress,
        TipoEquipamento.TransportadorCorreia => Icons.Material.Filled.LinearScale,
        _ => Icons.Material.Filled.Construction
    };
}
