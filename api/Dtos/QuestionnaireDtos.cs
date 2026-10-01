namespace QuestionnaireBuilder.Api.Dtos;

public record QuestionDto(
    int Id,
    string Type,
    string Text,
    string OptionsJson,
    int Order
);

public record CreateQuestionRequest(
    string Type,
    string Text,
    string OptionsJson,
    int Order
);

public record CreateQuestionnaireRequest(
    string Title,
    string Description,
    List<CreateQuestionRequest> Questions
);

public record QuestionnaireResponse(
    int Id,
    string Title,
    string Description,
    DateTime CreatedAt,
    List<QuestionDto> Questions
);
