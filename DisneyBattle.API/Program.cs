using DisneyBattle.API.Infrastructure;
using DisneyBattle.API.Models;
using DisneyBattle.BLL.Interfaces;
using DisneyBattle.BLL.Mapping;
using DisneyBattle.BLL.Models;
using DisneyBattle.BLL.Services;
using DisneyBattle.DAL;
using DisneyBattle.DAL.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
 
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.SetMinimumLevel(LogLevel.Warning);
// Add services to the container.
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
 
builder.Services.AddSwaggerGen(optionswag =>
{
    optionswag.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Disney Battle API",
        Description = "Exemple d'application Blazor .net8 + EFCore 8",
        Contact = new OpenApiContact
        {
            Name = "Contact",
            Url = new Uri("https://www.cognitic.be/contactez-nous")
        }


    });
    // using System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    optionswag.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

    //Jwt
    // Bearer token authentication


    optionswag.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    optionswag.OperationFilter<AddAuthHeaderOperationFilter>();


});
  
//MAspter
builder.Services.AddMapster();

// Configure Mapster
MapsterConfig.Configure(); //Mapper BLL
DisneyBattle.API.Mapping.MapsterConfig.Configure(); //Mapper API

/*Jwt*/
//Je récupère les infos de config de jwt à partir du
// fichier appsettings.json et je stocke le tout 
// dans la classe prévue 
JwtOptions options = builder.Configuration.GetSection("JwtOptions").Get<JwtOptions>();

//Pour pouvoir utiliser le jwtoption il faut l'injecter
builder.Services.AddSingleton(options);

//On configure l'authentication dans les services
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(
            o =>
            {
                //Je vais rechercher ma clé de signature
                byte[] sKey = Encoding.UTF8.GetBytes(options.SigningKey);

                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = options.Issuer,
                    ValidAudience = options.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(sKey)
                };
            }

    );
builder.Services.AddAuthorization();

builder.Services.AddDbContext<DisneyBattleContext>((sp, options) =>
{
#if DEBUG
    options.UseSqlServer(builder.Configuration.GetConnectionString("DisneyBattleDev"));
#else
    options.UseSqlServer(builder.Configuration.GetConnectionString("DisneyBattleProd"));
#endif
});
#region services
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserService, UtilisateurService>();

builder.Services.AddScoped<IService<PersonnageModel>, PersonnageService>();
#endregion




var corsOrigins = builder.Configuration
            .GetSection("CorsOrigins:AllowedOrigins")
            .Get<string[]>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("DefaultPolicy", policy =>
    {
        policy.WithOrigins(corsOrigins)
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials()
              .SetIsOriginAllowedToAllowWildcardSubdomains()  
              .WithExposedHeaders("Content-Disposition");
    });
});

// Configuration spécifique pour le développement
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("DevPolicy", policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
    });

}
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCors("DevPolicy");
    //app.UseSwagger();
    //app.UseSwaggerUI();
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(
      Path.Combine(Directory.GetCurrentDirectory(), "assets")),
        RequestPath = "/assets"
    });
    app.UseSwagger(options =>
    {
        options.SerializeAsV2 = true;
    });
    app.UseSwaggerUI(c =>
    {
        c.DocumentTitle = "DisneyBattle - Api for a Blazor App";
        c.InjectStylesheet("/assets/css/customswagger.css");
        c.InjectJavascript("/assets/swagger/iconswagger.js");
    });

}
else
{
    app.UseCors("DefaultPolicy");
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
