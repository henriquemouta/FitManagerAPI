using FitManager.Business;
using FitManager.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        npgsqlOptions => npgsqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(10),
            errorCodesToAdd: null
        )
    ));

// Repositórios
builder.Services.AddScoped<RepositorioUsuario>();
builder.Services.AddScoped<RepositorioTreino>();
builder.Services.AddScoped<RepositorioSessao>();
builder.Services.AddScoped<RepositorioExercicio>();

// Negócios
builder.Services.AddScoped<NegocioUsuario>();
builder.Services.AddScoped<NegocioTreino>();
builder.Services.AddScoped<NegocioSessao>();
builder.Services.AddScoped<NegocioExercicio>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "FitManager API v1");
    c.RoutePrefix = string.Empty;
});


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try
    {
        await db.Database.CanConnectAsync();
        Console.WriteLine("? Banco conectado com sucesso!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"? Erro ao conectar: {ex.Message}");
    }
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();