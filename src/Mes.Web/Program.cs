using Mes.Infrastructure;
using Mes.Web.Components;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMudServices();

// Banco SQLite: um arquivo mes.db dentro da pasta do projeto web.
var caminhoBanco = Path.Combine(builder.Environment.ContentRootPath, "mes.db");
builder.Services.AddDbContextFactory<MesDbContext>(options => options.UseSqlite($"Data Source={caminhoBanco}"));
builder.Services.AddScoped<EquipamentoService>();

var app = builder.Build();

// Cria/atualiza as tabelas (migrations) e insere os dados de exemplo.
using (var db = app.Services.GetRequiredService<IDbContextFactory<MesDbContext>>().CreateDbContext())
{
    db.Database.Migrate();
    DadosIniciais.Popular(db);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
