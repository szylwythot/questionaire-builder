using Microsoft.EntityFrameworkCore;
using QuestionnaireBuilder.Api.Models;

namespace QuestionnaireBuilder.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Questionnaire> Questionnaires => Set<Questionnaire>();
}
