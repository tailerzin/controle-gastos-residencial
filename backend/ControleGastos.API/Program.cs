using ControleGastos.API.Data;
using Microsoft.EntityFrameworkCore;
using ControleGastos.API.Interfaces.Repositories;
using ControleGastos.API.Repositories;
using ControleGastos.API.Interfaces.Services;
using ControleGastos.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Configura o banco SQLite usado pela API.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Habilita controllers e ajusta a serializacao JSON da API.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Evita erro ao retornar objetos que possuem relacionamentos entre si.
        options.JsonSerializerOptions.ReferenceHandler =
            System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;

        // Mostra enums como texto no JSON, em vez de apenas numeros.
        options.JsonSerializerOptions.Converters.Add(
            new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

// Libera chamadas feitas pelo frontend React durante o desenvolvimento.
builder.Services.AddCors(options =>
{
    options.AddPolicy("React",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddOpenApi();

// Registra as dependencias para que controllers usem services e repositories.
builder.Services.AddScoped<IPessoaRepository, PessoaRepository>();
builder.Services.AddScoped<IPessoaService, PessoaService>();
builder.Services.AddScoped<ITransacaoRepository, TransacaoRepository>();
builder.Services.AddScoped<ITransacaoService, TransacaoService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Em desenvolvimento, habilita Swagger/OpenAPI para testar os endpoints.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Aplica a politica CORS criada acima para aceitar chamadas do React.
app.UseCors("React");

app.UseAuthorization();

// Mapeia automaticamente as rotas dos controllers.
app.MapControllers();

app.Run();
