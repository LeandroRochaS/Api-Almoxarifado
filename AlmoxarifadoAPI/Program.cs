using AlmoxarifadoInfrastructure.Data;
using AlmoxarifadoInfrastructure.Data.Interfaces;
using AlmoxarifadoInfrastructure.Data.Repositories;
using AlmoxarifadoServices.Implementations;
using AlmoxarifadoServices.Implementations.EstoqueStrategy;
using AlmoxarifadoServices.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<IDbConnectionService, DbConnectionService>();
builder.Services.AddDbContext<xAlmoxarifadoContext>((serviceProvider, options) =>
{
    var dbConnectionService = serviceProvider.GetRequiredService<IDbConnectionService>();
    options.UseSqlServer(dbConnectionService.GetConnectionString());
});

builder.Services.AddAutoMapper(typeof(Program));

//Carregando Classes de Repositories
RepositoriesDependencies(builder.Services);
ServicesDependencies(builder.Services);

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "1.0",
        Title = "Almoxarifado API",
        Description = "API para gerenciamento de almoxarifado",
        Contact = new OpenApiContact
        {
            Name = "Leandro Rocha Santos",
            Email = "lerocha644@gmail.com",
            Url = new Uri("https://github.com/LeandroRochaS"),
        }
    });
});
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft
            .Json
            .ReferenceLoopHandling
            .Ignore
    );

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Almoxarifado API v1");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

void ServicesDependencies(IServiceCollection services)
{
    services.AddScoped<IGrupoService, GrupoService>();
    services.AddScoped<IProdutoService, ProdutoService>();
    services.AddScoped<INotaFiscalService, NotaFiscalService>();
    services.AddScoped<IClasseService, ClasseService>();
    services.AddScoped<IFornecedorService, FornecedorService>();
    services.AddScoped<ISecretariaService, SecretariaService>();
    services.AddScoped<IUnidadeDeMedidaService, UnidadeDeMedidaService>();
    services.AddScoped<IItemNotaService, ItemNotaService>();
    services.AddScoped<IGestaoNotaFiscalService, GestaoNotaFiscalService>();
    services.AddScoped<IClienteService, ClienteService>();
    services.AddScoped<ISetorService, SetorService>();
    services.AddScoped<IRequisicaoService, RequisicaoService>();
    services.AddScoped<IItemRequisicaoService, ItemRequisicaoService>();
    services.AddScoped<IEstoqueService, EstoqueService>();
    services.AddScoped<IGestaoRequisicaoService, GestaoRequisicaoService>();
}

void RepositoriesDependencies(IServiceCollection services)
{
    services.AddScoped<IGrupoRepository, GrupoRepository>();
    services.AddScoped<IClasseRepository, ClasseRepository>();
    services.AddScoped<IFornecedorRepository, FornecedorRepository>();
    services.AddScoped<ITipoDeNotaRepository, TipoDeNotaRepository>();
    services.AddScoped<INotaFiscalRepository, NotaFiscalRepository>();
    services.AddScoped<ISecretariaRepository, SecretariaRepository>();
    services.AddScoped<IUnidadeDeMedidaRepository, UnidadeDeMedidaRepository>();
    services.AddScoped<IProdutoRepository, ProdutoRepository>();
    services.AddScoped<ITipoDeNotaRepository, TipoDeNotaRepository>();
    services.AddScoped<IItemNotaRepository, ItemNotaRepository>();
    services.AddScoped<IClienteRepository, ClienteRepository>();
    services.AddScoped<ISetorRepository, SetorRepository>();
    services.AddScoped<IItemRequisicaoRepository, ItemRequisicaoRepository>();
    services.AddScoped<IRequisicaoRepository, RequisicaoRepository>();
    services.AddScoped<IEstoqueRepository, EstoqueRepository>();
}
