using Mes.Domain;

namespace Mes.Infrastructure;

/// <summary>
/// Popula o banco com um complexo minerador de exemplo na primeira execução.
/// </summary>
public static class DadosIniciais
{
    public static void Popular(MesDbContext db)
    {
        if (db.Sites.Any())
            return;

        var mina = new Area { Codigo = "MIN", Nome = "Mina" };
        var usina = new Area { Codigo = "USI", Nome = "Usina de Beneficiamento" };

        db.Sites.Add(new Site
        {
            Codigo = "SAZ",
            Nome = "Complexo Serra Azul",
            Areas = [mina, usina]
        });

        var caminhao = new ModeloEquipamento { Nome = "Caminhão fora de estrada 240 t", Fabricante = "Caterpillar", Tipo = TipoEquipamento.Caminhao, CapacidadeNominal = 240 };
        var escavadeira = new ModeloEquipamento { Nome = "Escavadeira hidráulica 42 m³", Fabricante = "Komatsu", Tipo = TipoEquipamento.Escavadeira, CapacidadeNominal = 70 };
        var perfuratriz = new ModeloEquipamento { Nome = "Perfuratriz rotativa", Fabricante = "Epiroc", Tipo = TipoEquipamento.Perfuratriz };
        var britador = new ModeloEquipamento { Nome = "Britador primário giratório", Fabricante = "Metso", Tipo = TipoEquipamento.Britador, CapacidadeNominal = 8000 };
        var correia = new ModeloEquipamento { Nome = "Transportador de correia 72\"", Fabricante = "Continental", Tipo = TipoEquipamento.TransportadorCorreia, CapacidadeNominal = 9000 };

        db.Modelos.AddRange(caminhao, escavadeira, perfuratriz, britador, correia);

        db.Equipamentos.AddRange(
            new Equipamento { Tag = "CM-101", Modelo = caminhao, Area = mina, Estado = EstadoEquipamento.Operando },
            new Equipamento { Tag = "CM-102", Modelo = caminhao, Area = mina, Estado = EstadoEquipamento.ParadaOperacional },
            new Equipamento { Tag = "CM-103", Modelo = caminhao, Area = mina, Estado = EstadoEquipamento.Manutencao },
            new Equipamento { Tag = "CM-104", Modelo = caminhao, Area = mina, Estado = EstadoEquipamento.Manutencao },
            new Equipamento { Tag = "EH-201", Modelo = escavadeira, Area = mina, Estado = EstadoEquipamento.Operando },
            new Equipamento { Tag = "EH-202", Modelo = escavadeira, Area = mina, Estado = EstadoEquipamento.ParadaOperacional },
            new Equipamento { Tag = "PF-501", Modelo = perfuratriz, Area = mina, Estado = EstadoEquipamento.Operando },
            new Equipamento { Tag = "BR-301", Modelo = britador, Area = usina, Estado = EstadoEquipamento.Operando },
            new Equipamento { Tag = "TC-401", Modelo = correia, Area = usina, Estado = EstadoEquipamento.Reserva });

        db.SaveChanges();
    }
}
