using Mes.Domain;
using Mes.Infrastructure;

namespace Mes.Tests;

public class EquipamentoServiceTests : IDisposable
{
    private readonly BancoDeTeste _banco = new();
    private readonly EquipamentoService _servico;

    public EquipamentoServiceTests()
    {
        _servico = new EquipamentoService(_banco);
    }

    public void Dispose() => _banco.Dispose();

    /// <summary>Monta um equipamento válido usando o primeiro modelo e área do banco.</summary>
    private async Task<Equipamento> NovoEquipamentoValido(string tag)
    {
        var modelo = (await _servico.ListarModelosAsync())[0];
        var area = (await _servico.ListarAreasAsync())[0];
        return new Equipamento { Tag = tag, ModeloId = modelo.Id, AreaId = area.Id };
    }

    [Fact]
    public async Task Salvar_EquipamentoValido_ApareceNaListagem()
    {
        await _servico.SalvarAsync(await NovoEquipamentoValido("CM-999"));

        var lista = await _servico.ListarAsync();
        Assert.Contains(lista, e => e.Tag == "CM-999");
    }

    [Fact]
    public async Task Salvar_TagComEspacosEMinusculas_GravaNormalizada()
    {
        await _servico.SalvarAsync(await NovoEquipamentoValido("  cm-105 "));

        var lista = await _servico.ListarAsync();
        Assert.Contains(lista, e => e.Tag == "CM-105");
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public async Task Salvar_TagVazia_LancaErro(string tag)
    {
        var equipamento = await NovoEquipamentoValido(tag);

        var erro = await Assert.ThrowsAsync<RegraDeNegocioException>(() => _servico.SalvarAsync(equipamento));
        Assert.Equal("A tag é obrigatória.", erro.Message);
    }

    [Fact]
    public async Task Salvar_TagMaiorQueOLimite_LancaErro()
    {
        var equipamento = await NovoEquipamentoValido(new string('X', EquipamentoService.TamanhoMaximoTag + 1));

        await Assert.ThrowsAsync<RegraDeNegocioException>(() => _servico.SalvarAsync(equipamento));
    }

    [Fact]
    public async Task Salvar_TagDuplicada_LancaErro()
    {
        // CM-101 já existe nos dados iniciais; minúsculo para provar que não diferencia caixa.
        var equipamento = await NovoEquipamentoValido("cm-101");

        var erro = await Assert.ThrowsAsync<RegraDeNegocioException>(() => _servico.SalvarAsync(equipamento));
        Assert.Equal("Já existe um equipamento com a tag CM-101.", erro.Message);
    }

    [Fact]
    public async Task Salvar_EditandoSemTrocarATag_NaoAcusaDuplicidade()
    {
        var existente = (await _servico.ListarAsync()).First(e => e.Tag == "CM-101");
        existente.Estado = EstadoEquipamento.Manutencao;

        await _servico.SalvarAsync(existente);

        var atualizado = (await _servico.ListarAsync()).First(e => e.Tag == "CM-101");
        Assert.Equal(EstadoEquipamento.Manutencao, atualizado.Estado);
    }

    [Fact]
    public async Task Excluir_EquipamentoExistente_SomeDaListagem()
    {
        var existente = (await _servico.ListarAsync()).First(e => e.Tag == "TC-401");

        await _servico.ExcluirAsync(existente.Id);

        Assert.DoesNotContain(await _servico.ListarAsync(), e => e.Tag == "TC-401");
    }

    [Fact]
    public async Task Salvar_SemModelo_LancaErro()
    {
        // Equipamento sendo cadastrado com Modelo igual a 0.
        var equipamento = await NovoEquipamentoValido("cm-777");

        equipamento.ModeloId = 0;

        var erro = await Assert.ThrowsAsync<RegraDeNegocioException>(() => _servico.SalvarAsync(equipamento));
        Assert.Equal("Selecione o modelo do equipamento.", erro.Message);
    }
}
