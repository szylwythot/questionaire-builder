namespace QuestionnaireBuilder.Api.Models;

public enum QuestionType
{
    Text = 0,
    Checkbox = 1,
}

public class Question
{
    public int Id { get; set; }
    public QuestionType Type { get; set; }
    public string Text { get; set; } = string.Empty;
    public string OptionsJson { get; set; } = string.Empty;  // "Yes","No" as JSON
    public int QuestionnaireId { get; set; }
    public int Order { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    

    public Questionnaire Questionnaire { get; set; } = null!;
}