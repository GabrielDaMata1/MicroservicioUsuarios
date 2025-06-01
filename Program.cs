using MassTransit;
using MassTransit.Mediator;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Microsoft.OpenApi.Models;
using MicroserviciosUsuarios.Infrastructure.Persistance;
using MicroserviciosUsuarios.Infrastructure.Repositories.MongoDB;
using MicroserviciosUsuarios.Infrastructure.Repositories.PostgreSQL;
using MicroserviciosUsuarios.Infrastructure.Repositories.Keycloak;
using MicroserviciosUsuarios.Domain.Events;
using MicroserviciosUsuarios.Infrastructure.Consumers;
using MicroserviciosUsuarios.Application.Handler;
using MicroservicioUsuarios.Application.Handler;
using MicroservicioUsuarios.Infrastructure.Services;
using MicroservicioUsuarios.Application.Services;

using MicroservicioUsuarios.WebAPIUsuarios.Controllers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Mi API",
        Version = "v1",
        Description = "Documentaci�n de mi API usando Swagger"
    });
});


// Configuraci�n de PostgreSQL
builder.Services.AddDbContext<SubastaDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioMongoRepository, UsuarioMongoRepository>();
builder.Services.AddScoped<IHistorialActividadRepository, HistorialActividadRepository>();
builder.Services.AddScoped<IHistorialActividadMongoRepository, HistorialActividadMongoRepository>();
builder.Services.AddScoped<IKeycloakRepository, KeycloakRepository>();
builder.Services.AddScoped<IRolRepository, RolRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IHistorialActividadServices, HistorialActividadServices>();
builder.Services.AddHttpClient<KeycloakAuthService>();
// Configuraci�n de MongoDB
var mongoClient = new MongoClient("mongodb://localhost:27017");
builder.Services.AddSingleton<IMongoClient>(mongoClient);

// Configuraci�n de RabbitMQ con MassTransit
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<UsuarioRegistradoConsumer>();
    x.AddConsumer<UsuarioModificadoConsumer>();
    x.AddConsumer<ActividadRegistradaConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq://localhost", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint("usuarios-registrados-queue", e =>
        {
            e.ConfigureConsumer<UsuarioRegistradoConsumer>(context);
        });

        cfg.ReceiveEndpoint("usuarios-modificados-queue", e =>
        {
            e.ConfigureConsumer<UsuarioModificadoConsumer>(context);
        });

        cfg.ReceiveEndpoint("actividad-registrada-queue", e =>
        {
            e.ConfigureConsumer<ActividadRegistradaConsumer>(context);
        });
    });
});

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<RegistrarUsuarioHandler>());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<ConsultarCorreoHandler>());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<ActualizarContraseñaHandler>());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<ActualizarPerfilUsuarioHandler>());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<ConsultarUsuariosHandler>());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<AsignarRolHandler>());




var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mi API v1");
    });
}
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();


