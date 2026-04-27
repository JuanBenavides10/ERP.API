using ERP.Accounting.Application.Interfaces;
using ERP.Accounting.Application.Interfaces.Configuration;
using ERP.Accounting.Application.Mappings;
using ERP.Accounting.Application.Services;
using ERP.Accounting.Application.Services.Configuration;
using ERP.Accounting.Infrastructure.Persistence.DbContexts;
using ERP.Accounting.Infrastructure.Persistence.Repositories;
using ERP.Accounting.Infrastructure.Persistence.Repositories.Configuration;
using ERP.Api.Presentation.Contracts.Responses;
using ERP.Identity.Infrastructure.Persistence.DbContexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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


//AutoMapper -> para mapear de las entidades a los DTOs o viceverca
builder.Services.AddAutoMapper(cfg => { }, typeof(AnexosProfile));

//Cambiamos el codigo de error Http del ModelState , de 400 a 422 segun se acordo
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        // Agrupar todos los mensajes de error por campo
        var errores = context.ModelState
            .Where(e => e.Value.Errors.Count > 0)
            .GroupBy(
                e => e.Key,
                e => e.Value.Errors.Select(error => error.ErrorMessage).ToList()
            )
            .ToDictionary(
                g => g.Key,
                g => g.SelectMany(x => x).ToList()
            );

        var response422 = new ApiResponse<object>
        {
            Success = false,
            StatusCode = 422,
            Message = "Error de validación",
            Errors = errores
        };

        return new UnprocessableEntityObjectResult(response422);
    };
});

//DI

builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
