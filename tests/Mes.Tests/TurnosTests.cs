using Mes.Domain;

namespace Mes.Tests;

public class TurnosTests
{
    [Theory]
    [InlineData("06:00", "A")]
    [InlineData("13:59", "A")]
    [InlineData("14:00", "B")]
    [InlineData("21:59", "B")]
    [InlineData("22:00", "C")]
    [InlineData("00:00", "C")]
    [InlineData("05:59", "C")]
    public void Identificar_HorarioNaFronteira_RetornaTurnoCorreto(string hora, string turnoEsperado)
    {
        Assert.Equal(turnoEsperado, Turnos.Identificar(TimeOnly.Parse(hora)));
    }
}
