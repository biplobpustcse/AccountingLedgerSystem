using AccountingLedgerSystem.Application.Commands.Accounts;
using AccountingLedgerSystem.Application.Mapping;
using AccountingLedgerSystem.Infrastructure;
using AccountingLedgerSystem.Infrastructure.DbInitializer;
using AccountingLedgerSystem.Persistence.Data;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// dependency injection container
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection"))
);

builder.Services.AddInfrastructure();
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddMediatR(typeof(CreateAccountHandler).Assembly);

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssembly(typeof(CreateAccountValidator).Assembly);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.Services.EnsureStoredProcedures();
app.MapControllers();
app.Run();
