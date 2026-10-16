namespace Mes.Domain;

/// <summary>
/// Escala de três turnos de 8 horas: A (06h–14h), B (14h–22h) e C (22h–06h).
/// </summary>
public static class Turnos
{
    public static string Identificar(TimeOnly hora) => hora.Hour switch
    {
        >= 6 and < 14 => "A",
        >= 14 and < 22 => "B",
        _ => "C"
    };
}
