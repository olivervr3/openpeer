using Microsoft.EntityFrameworkCore;
using OpenPeer.Infrastructure;
using MediatR;
using FluentValidation;
using OpenPeer.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(typeof(AssemblyMarker));
builder.Services.AddValidatorsFromAssembly(typeof(AssemblyMarker).Assembly);

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// EF Core (SQL Server)
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// Health
app.MapGet("/health", () => Results.Ok(new { status = "ok" }));

app.Run();
