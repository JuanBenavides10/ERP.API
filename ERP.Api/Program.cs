using ERP.Accounting.Application.Interfaces.Configuration;
using ERP.Accounting.Application.Mappings.Configuration;
using ERP.Accounting.Application.Services.Configuration;
using ERP.Accounting.Infrastructure.Persistence.DbContexts;
using ERP.Accounting.Infrastructure.Persistence.Repositories.Configuration;
using ERP.Api.Middleware;
using ERP.Api.Presentation.Contracts;
using ERP.Identity.Application.Interfaces;
using ERP.Identity.Application.Services;
using ERP.Identity.Infrastructure.Persistence.DbContexts;
using ERP.Identity.Infrastructure.Persistence.Repositories;
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
builder.Services.AddAutoMapper(cfg => { }, typeof(CompanyProfile));

//Cambiamos el codigo de error Http del ModelState , de 400 a 422 segun se acordo

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        // Construye: { "Campo": ["error1", "error2"] }
        var errores = context.ModelState
            .Where(kvp => kvp.Value is not null && kvp.Value.Errors.Count > 0)
            .ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
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


//DI

builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
