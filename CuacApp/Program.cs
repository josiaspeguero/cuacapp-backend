using CuacApp.Application.Services;
using CuacApp.Application.Use_Case;
using CuacApp.Data;
using CuacApp.Data.Repositories;
using CuacApp.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//dbcontext
builder.Services.AddDbContext<ApplicationDbContext>(get => get.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")));

//automapper
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<AutoMapperProfile>();
});

//servicios
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<ICodigoUsuario, CodigoUsuarioRepository>();

//casos de uso
builder.Services.AddScoped<RegistrarUsuario>();
builder.Services.AddScoped<ValidarCodigosUsuario>();
builder.Services.AddScoped<CambiarContrasena>();
builder.Services.AddScoped<IniciarSesion>();

//transients
builder.Services.AddTransient<IEnviarMensaje, EnviarMensaje>();

//cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("global", policy =>
    {
        policy.WithOrigins("http://localhost:3000") // Permite solo este origen
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("global");
app.UseAuthorization();

app.MapControllers();

app.Run();
