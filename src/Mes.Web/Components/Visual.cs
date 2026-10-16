using Mes.Domain;
using MudBlazor;

namespace Mes.Web.Components;

/// <summary>
/// Cores, classes CSS e ícones usados em todas as telas, para manter o visual consistente.
/// As cores dos estados ficam em wwwroot/mes.css.
/// </summary>
public static class Visual
{
    public static string ClasseDoEstado(EstadoEquipamento estado) => estado switch
    {
        EstadoEquipamento.Operando => "estado-operando",
        EstadoEquipamento.ParadaOperacional => "estado-parada",
        EstadoEquipamento.Manutencao => "estado-manutencao",
        _ => "estado-reserva"
    };

    public static string IconeDoEstado(EstadoEquipamento estado) => estado switch
    {
        EstadoEquipamento.Operando => Icons.Material.Rounded.PlayCircle,
        EstadoEquipamento.ParadaOperacional => Icons.Material.Rounded.PauseCircle,
        EstadoEquipamento.Manutencao => Icons.Material.Rounded.Build,
        _ => Icons.Material.Rounded.HourglassEmpty
    };

    public static string IconeDoTipo(TipoEquipamento tipo) => tipo switch
    {
        TipoEquipamento.Caminhao => Icons.Material.Rounded.LocalShipping,
        TipoEquipamento.Escavadeira => Icons.Material.Rounded.Agriculture,
        TipoEquipamento.Carregadeira => Icons.Material.Rounded.Agriculture,
        TipoEquipamento.Perfuratriz => Icons.Material.Rounded.Hardware,
        TipoEquipamento.Britador => Icons.Material.Rounded.Compress,
        TipoEquipamento.TransportadorCorreia => Icons.Material.Rounded.LinearScale,
        _ => Icons.Material.Rounded.Construction
    };

    /// <summary>Percentual inteiro de <paramref name="parte"/> sobre <paramref name="total"/>.</summary>
    public static int Percentual(int parte, int total) => total == 0 ? 0 : (int)Math.Round(100.0 * parte / total);
}
