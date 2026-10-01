using Microsoft.EntityFrameworkCore;
using QuestionnaireBuilder.Api.Data;
using QuestionnaireBuilder.Api.Dtos;
using QuestionnaireBuilder.Api.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlite(builder.Configuration.GetConnectionString("Default")));


// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var questionnaires = app.MapGroup("/api/questionnaires");

questionnaires.MapPost("/", async (CreateQuestionnaireRequest req, AppDbContext db) =>
{
    if (string.IsNullOrWhiteSpace(req.Title) || req.Title.Length > 255)
        return Results.ValidationProblem(new Dictionary<string, string[]>
        {
            { "title", new[] { "Title is required and must be 1-255 characters" } }
        });

    if (req.Questions == null || req.Questions.Count == 0)
        return Results.ValidationProblem(new Dictionary<string, string[]>
        {
            { "questions", new[] { "At least one question is required" } }
        });

    foreach (var q in req.Questions)
    {
        if (string.IsNullOrWhiteSpace(q.Text) || q.Text.Length > 1000)
            return Results.ValidationProblem(new Dictionary<string, string[]>
            {
                { "questions", new[] { "Each question text must be 1-1000 characters" } }
            });

        if (!Enum.TryParse<QuestionType>(q.Type, out _))
            return Results.ValidationProblem(new Dictionary<string, string[]>
            {
                { "questions", new[] { $"Invalid question type: {q.Type}" } }
            });
    }

    var questionnaire = new Questionnaire
    {
        Title = req.Title,
        Description = req.Description ?? "",
        CreatedAt = DateTime.UtcNow,
        Questions = req.Questions.Select((q, i) => new Question
        {
            Type = Enum.Parse<QuestionType>(q.Type),
            Text = q.Text,
            OptionsJson = q.OptionsJson ?? "",
            Order = i,
            CreatedAt = DateTime.UtcNow
        }).ToList()
    };

    db.Questionnaires.Add(questionnaire);
    await db.SaveChangesAsync();

    var response = new QuestionnaireResponse(
        questionnaire.Id,
        questionnaire.Title,
        questionnaire.Description,
        questionnaire.CreatedAt,
        questionnaire.Questions.Select(q => new QuestionDto(
            q.Id,
            q.Type.ToString(),
            q.Text,
            q.OptionsJson,
            q.Order
        )).ToList()
    );

    return Results.Created($"/api/questionnaires/{questionnaire.Id}", response);
});

app.MapGet("/api/health", async (AppDbContext db) =>
    new { questionnaires = await db.Questionnaires.CountAsync() });

app.Run();
