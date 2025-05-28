using System.Reflection;
using System.Text.Json.Serialization;
using API.Setup;
using Infra.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

#region ConfigureServices
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});


// Carrega o .env manualmente
DotNetEnv.Env.Load("../config/.env");

var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

builder.Services.AddDbContext<DataContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddControllers()
       .AddJsonOptions(opts =>
       {
           opts.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
           opts.JsonSerializerOptions.WriteIndented = true;
       });

builder.Services.RegisterService();
builder.Services.RegisterRepository();
builder.Services.RegisterAutoMapper();

#endregion

var app = builder.Build();

#region configure

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
    dbContext.Database.Migrate();
}
#endregion

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<DataContext>();

    try
    {
        // Tenta abrir conexão com o banco
        await context.Database.OpenConnectionAsync();
        Console.WriteLine("Conexão com o banco MySQL");
        await context.Database.CloseConnectionAsync();
    }
    catch (Exception ex)
    {
        Console.WriteLine("Falha ao conectar no banco: " + ex.Message);
    }
}

app.Run();
