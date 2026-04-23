using Microsoft.EntityFrameworkCore;
using PlataformaEventosTech_API.Data;
using PlataformaEventosTech_API.Repositories;
using PlataformaEventosTech_API.Repositories.Interfaces;
using PlataformaEventosTech_API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

// Swagger + documentação XML
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    options.IncludeXmlComments(xmlPath);
});

// Configuração do Entity Framework Core para usar o SQLite como banco de dados
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=Plataforma.db"));

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));


// Configuração de injeção de dependência para os repositórios e serviços
builder.Services.AddScoped<IPlataformaRepository, PlataformaRepository>();
builder.Services.AddScoped<PlataformaService>();

var app = builder.Build();


// url personalizada para acessar a documentação - http://localhost:5000/documentacao
app.UseSwagger();
app.UseSwaggerUI();
app.UseSwaggerUI(options =>
{
    options.RoutePrefix = "documentação";
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Plataforma de Eventos Tech API v1");
});


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
