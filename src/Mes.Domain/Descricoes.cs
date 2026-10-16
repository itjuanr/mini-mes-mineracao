namespace Mes.Domain;

/// <summary>
/// Textos amigáveis para mostrar os enums na tela.
/// </summary>
public static class Descricoes
{
    public static string Descricao(this EstadoEquipamento estado) => estado switch
    {
        EstadoEquipamento.Operando => "Operando",
        EstadoEquipamento.ParadaOperacional => "Parada operacional",
        EstadoEquipamento.Manutencao => "Manutenção",
        EstadoEquipamento.Reserva => "Reserva",
        _ => estado.ToString()
    };

    public static string Descricao(this TipoEquipamento tipo) => tipo switch
    {
        TipoEquipamento.Caminhao => "Caminhão",
        TipoEquipamento.Escavadeira => "Escavadeira",
        TipoEquipamento.Carregadeira => "Carregadeira",
        TipoEquipamento.Perfuratriz => "Perfuratriz",
        TipoEquipamento.Britador => "Britador",
        TipoEquipamento.TransportadorCorreia => "Transportador de correia",
        _ => tipo.ToString()
    };
}
