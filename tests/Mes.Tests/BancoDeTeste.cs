using Mes.Infrastructure;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Mes.Tests;

/// <summary>
/// Banco SQLite em memória, criado do zero para cada teste e populado
/// com os dados iniciais. Some quando o teste termina.
/// </summary>
public sealed class BancoDeTeste : IDbContextFactory<MesDbContext>, IDisposable
{
    private readonly SqliteConnection _conexao;
    private readonly DbContextOptions<MesDbContext> _opcoes;

    public BancoDeTeste()
    {
        _conexao = new SqliteConnection("Data Source=:memory:");
        _conexao.Open();
        _opcoes = new DbContextOptionsBuilder<MesDbContext>().UseSqlite(_conexao).Options;

        using var db = CreateDbContext();
        db.Database.EnsureCreated();
        DadosIniciais.Popular(db);
    }

    public MesDbContext CreateDbContext() => new(_opcoes);

    public void Dispose() => _conexao.Dispose();
}
