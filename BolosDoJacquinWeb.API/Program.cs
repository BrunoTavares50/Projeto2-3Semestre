using BolosDoJacquinWeb.API.BdContextEvent;
using BolosDoJacquinWeb.API.Interfaces;
using BolosDoJacquinWeb.API.Models;
using BolosDoJacquinWeb.API.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//Adicionando Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "Insira um token válido para ter acesso aos endpoint da API"
    });

    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("Bearer", document)] = []
    });
});

//Configuração do EFCore - Banco de dados
builder.Services.AddDbContext<BolosJacquinContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

//Injeção de dependência
//AddScoped significa que uma instância nova é criada por requisição http
//Isso garante que cada requisição tenha seu próprio contexto isolado
builder.Services.AddScoped<ITipoUsuario, TipoUsuarioRepository>();
builder.Services.AddScoped<IUsuario, UsuarioRepository>();
builder.Services.AddScoped<IProduto, ProdutoRepository>();
builder.Services.AddScoped<IAvaliacao, AvaliacaoRepository>();
builder.Services.AddScoped<ICategoria, CategoriaRepository>();

//AUTENTICAÇÃO JWT
//Configura como a API vai validar os tokens recebidos nas requisições
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        //valida quem emitiu o token
        ValidateIssuer = true,
        ValidIssuer = "BolosDoJacquin.WebAPI",

        //valida para quem o token foi emitido
        ValidateAudience = true,
        ValidAudience = "BolosDoJacquin.WebAPI",

        //valida se o token ainda está dentro do prazo de validade
        ValidateLifetime = true,

        //define a tolerância de clock entre servidores
        ClockSkew = TimeSpan.FromMinutes(5),

        //chave secreta utilizada para validar a assinatura do token
        IssuerSigningKey = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes("Jwt:Key")
        )
    };
});

////Configuração do Cloudinary
//builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("Cloudinary"));

////---Sightengine(plano Free, sem cartão)---
//builder.Services.Configure<SightengineSettings>(builder.Configuration.GetSection("Sightengine"));

//builder.Services.AddHttpClient<IModerationService, SightengineModerationService>(client =>
//{
//    client.BaseAddress = new Uri("https://api.sightengine.com/1.0/");
//});

//Registra o serviço de controllers(mapeia automaticamente os controllers da pasta /Controllers)
builder.Services.AddControllers();

builder.Services.AddOpenApi();

//Registra o serviço de autorização (necessário para [Authorize] funcionar)
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
