using ERP.Accounting.Application.Interfaces.Company;
using ERP.Accounting.Application.Interfaces.Storage;
using ERP.Accounting.Application.Mappings.Company;
using ERP.Accounting.Application.Services.Company;
using ERP.Accounting.Infrastructure.Persistence.DbContexts;
using ERP.Accounting.Infrastructure.Persistence.Repositories.Company;
using ERP.Accounting.Infrastructure.Storage;
using ERP.Api.Middleware;
using ERP.Api.Presentation.Contracts;
using ERP.Identity.Application.Interfaces;
using ERP.Identity.Application.Security;
using ERP.Identity.Application.Services;
using ERP.Identity.Infrastructure.Persistence.DbContexts;
using ERP.Identity.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ERP.Identity.Application.Mappings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Conexion con PostgreSQL
builder.Services.AddDbContext<AccountingDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("PostgreSQL")
    )
);

builder.Services.AddDbContext<IdentityDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("PostgreSQL")
    )
);

//Cambiamos el codigo de error Http del ModelState , de 400 a 422 segun se acordo
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errores = context.ModelState
            .Where(kvp => kvp.Value is not null && kvp.Value.Errors.Count > 0)
            .ToDictionary(
                //kvp => kvp.Key,
                //kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).ToArray()

                kvp => string.IsNullOrWhiteSpace(kvp.Key) ? "Detail" : kvp.Key,
                kvp => kvp.Value!.Errors.Select(e =>
                {
                    var msg = string.IsNullOrWhiteSpace(e.ErrorMessage) ? "Error de validación." : e.ErrorMessage;

                    if (msg.Contains("Failed to read the request form", StringComparison.OrdinalIgnoreCase) || msg.Contains("Request body too large", StringComparison.OrdinalIgnoreCase))
                    {
                        msg = "El archivo o el contenido enviado supera el tamaño permitido.";
                    }
                    return msg;
                }).ToArray()
            );

        var response422 = new ApiResponse<object?>
        {
            Success = false,
            StatusCode = StatusCodes.Status422UnprocessableEntity,
            Message = "Error de validación",
            Data = null,
            Errors = errores
        };

        return new UnprocessableEntityObjectResult(response422);
    };
});

//AutoMapper -> para mapear de las entidades a los DTOs o viceverca
builder.Services.AddAutoMapper(cfg => { }, typeof(CompanyProfile));
builder.Services.AddAutoMapper(cfg => { }, typeof(IdentityProfile));
//IMG

builder.Services.Configure<FileStorageOptions>(builder.Configuration.GetSection("FileStorage"));
builder.Services.AddScoped<IFileStorage, FileStorage>();


//DI

builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();


//JWT
builder.Services.AddScoped<JwtHelper>();


var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

builder.Services.AddAuthorization();


var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
