using Microsoft.EntityFrameworkCore;
using OpenPeer.Infrastructure;
using MediatR;
using FluentValidation;
using FluentValidation.Results;
using OpenPeer.Application;
using OpenPeer.Application.Articles.Commands;
using OpenPeer.Application.Articles.Queries;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<AssemblyMarker>();
});
builder.Services.AddValidatorsFromAssembly(typeof(AssemblyMarker).Assembly);

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// EF Core (SQL Server)
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

var app = builder.Build();

var articles = app.MapGroup("/articles");

app.UseSwagger();
app.UseSwaggerUI();

// Health
app.MapGet("/health", () => Results.Ok(new { status = "ok" }));

// POST /articles
articles.MapPost("/", async (CreateArticle cmd, IMediator mediator, IValidator<CreateArticle> validator) =>
{
    // Validación explícita
    ValidationResult result = await validator.ValidateAsync(cmd);
    if (!result.IsValid)
    {
        var errors = result.Errors
            .GroupBy(e => e.PropertyName)
            .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());

        return Results.ValidationProblem(errors);
    }

    var id = await mediator.Send(cmd);
    return Results.Created($"/articles/{id}", new { id });
});

// GET /articles/{id}
articles.MapGet("/{id:int}", async (int id, IMediator mediator) =>
{
    var dto = await mediator.Send(new GetArticleById(id));
    return dto is null ? Results.NotFound() : Results.Ok(dto);
});

// GET /articles?page=1&pageSize=10
articles.MapGet("/", async (int page, int pageSize, IMediator mediator) =>
{
    var list = await mediator.Send(new GetArticlesPaged(page, pageSize));
    return Results.Ok(list);
});

app.Run();
