using Mes.Domain;
using Microsoft.EntityFrameworkCore;

namespace Mes.Infrastructure;

/// <summary>
/// Operações de cadastro de equipamentos e as regras de negócio que as protegem.
/// As telas nunca falam direto com o banco: passam sempre por aqui.
/// </summary>
public class EquipamentoService(IDbContextFactory<MesDbContext> fabrica)
{
    public const int TamanhoMaximoTag = 20;

    public async Task<List<Equipamento>> ListarAsync()
    {
        await using var db = await fabrica.CreateDbContextAsync();
        return await db.Equipamentos
            .Include(e => e.Modelo)
            .Include(e => e.Area).ThenInclude(a => a.Site)
            .OrderBy(e => e.Tag)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<ModeloEquipamento>> ListarModelosAsync()
    {
        await using var db = await fabrica.CreateDbContextAsync();
        return await db.Modelos.OrderBy(m => m.Nome).AsNoTracking().ToListAsync();
    }

    public async Task<List<Area>> ListarAreasAsync()
    {
        await using var db = await fabrica.CreateDbContextAsync();
        return await db.Areas.Include(a => a.Site)
            .OrderBy(a => a.Site.Nome).ThenBy(a => a.Nome)
            .AsNoTracking()
            .ToListAsync();
    }

    /// <summary>Hierarquia completa Site → Área → Equipamento.</summary>
    public async Task<List<Site>> ListarEstruturaAsync()
    {
        await using var db = await fabrica.CreateDbContextAsync();
        return await db.Sites
            .Include(s => s.Areas).ThenInclude(a => a.Equipamentos).ThenInclude(e => e.Modelo)
            .OrderBy(s => s.Nome)
            .AsNoTracking()
            .ToListAsync();
    }

    /// <summary>
    /// Cria (Id = 0) ou atualiza um equipamento.
    /// RN-001: a tag é obrigatória e tem no máximo 20 caracteres.
    /// RN-002: a tag é única (sem diferenciar maiúsculas/minúsculas).
    /// RN-003: o equipamento precisa de um modelo e de uma área válidos.
    /// </summary>
    public async Task SalvarAsync(Equipamento dados)
    {
        var tag = (dados.Tag ?? string.Empty).Trim().ToUpperInvariant();

        if (tag.Length == 0)
            throw new RegraDeNegocioException("A tag é obrigatória.");
        if (tag.Length > TamanhoMaximoTag)
            throw new RegraDeNegocioException($"A tag deve ter no máximo {TamanhoMaximoTag} caracteres.");

        await using var db = await fabrica.CreateDbContextAsync();

        if (await db.Equipamentos.AnyAsync(e => e.Tag == tag && e.Id != dados.Id))
            throw new RegraDeNegocioException($"Já existe um equipamento com a tag {tag}.");
        if (!await db.Modelos.AnyAsync(m => m.Id == dados.ModeloId))
            throw new RegraDeNegocioException("Selecione o modelo do equipamento.");
        if (!await db.Areas.AnyAsync(a => a.Id == dados.AreaId))
            throw new RegraDeNegocioException("Selecione a área do equipamento.");

        Equipamento equipamento;
        if (dados.Id == 0)
        {
            equipamento = new Equipamento();
            db.Equipamentos.Add(equipamento);
        }
        else
        {
            equipamento = await db.Equipamentos.FindAsync(dados.Id)
                ?? throw new RegraDeNegocioException("Equipamento não encontrado.");
        }

        equipamento.Tag = tag;
        equipamento.ModeloId = dados.ModeloId;
        equipamento.AreaId = dados.AreaId;
        equipamento.Estado = dados.Estado;
        equipamento.Ativo = dados.Ativo;

        await db.SaveChangesAsync();
    }

    public async Task ExcluirAsync(int id)
    {
        await using var db = await fabrica.CreateDbContextAsync();
        var equipamento = await db.Equipamentos.FindAsync(id)
            ?? throw new RegraDeNegocioException("Equipamento não encontrado.");

        db.Equipamentos.Remove(equipamento);
        await db.SaveChangesAsync();
    }
}
