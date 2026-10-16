# Mini MES · Mineração

Sistema MES (Manufacturing Execution System) simplificado para mineração: apontamento de estados de equipamentos, paradas, produção, consumo e indicadores operacionais (DF, UF, MTBF/MTTR), alimentado por um simulador de frota.

> 🚧 Em desenvolvimento — projeto de portfólio construído em fases.

## Stack

- **.NET 8 / C#**
- **Blazor Server + MudBlazor** (interface)
- **Entity Framework Core + SQLite** (persistência, com migrations)
- **xUnit** (testes)

## Estrutura

```
src/
  Mes.Domain/          regras de negócio (equipamentos, estados, paradas, turnos)
  Mes.Infrastructure/  banco de dados (EF Core) e serviços
  Mes.Simulator/       simulador da frota gerando eventos
  Mes.Web/        interface web e API
tests/
  Mes.Tests/      testes automatizados
```

## Como rodar

Pré-requisito: [SDK do .NET 8](https://dotnet.microsoft.com/download/dotnet/8.0)

```bash
dotnet run --project src/Mes.Web
```

Acesse http://localhost:5290

## Roadmap

- [x] **Fase 0** — Estrutura do projeto e painel inicial
- [x] **Fase 1** — Cadastro de ativos (Site → Área → Equipamento)
- [ ] **Fase 2** — Simulador de frota (máquina de estados)
- [ ] **Fase 3** — Painel em tempo real
- [ ] **Fase 4** — Paradas e árvore de motivos
- [ ] **Fase 5** — Produção e consumo por turno
- [ ] **Fase 6** — Indicadores (DF, UF, MTBF, MTTR)
- [ ] **Fase 7** — Testes automatizados, Docker e CI
