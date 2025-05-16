using Microsoft.EntityFrameworkCore;
using Senai_Notas.Context;
using Senai_Notas.Interfaces;
using Senai_Notas.Repositories;
using Senai_Notas.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configuração do contexto do banco de dados
builder.Services.AddDbContext<ProjetoSenaiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registro dos repositórios
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<INotaRepository, NotaRepository>();
builder.Services.AddScoped<IAnexoRepository, AnexoRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<INotaTagRepository, NotaTagRepository>();

// Registro dos serviços auxiliares
builder.Services.AddScoped<PasswordService>();

// Configuração do TokenService com chave secreta do appsettings.json
var secretKey = builder.Configuration["Jwt:Key"] ?? "chave-secreta-supersegura";
builder.Services.AddSingleton(new TokenService(secretKey));

// Configuração do JWT Bearer
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey))
    };
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuração do CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontEndPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:3000") // Altere para o endereço do seu front-end
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("FrontEndPolicy");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.RoutePrefix = string.Empty; // Swagger UI na raiz
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Senai Notas API v1");
});

app.MapControllers();
app.Run();
