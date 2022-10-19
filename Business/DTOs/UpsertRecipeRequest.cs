using Persistence.Entities.Enums;

namespace Business.DTOs;

public record UpsertRecipeRequest(
    string Name,
    string Description,
    MealType MealType,
    DifficultyLevel Difficulty,
    string TotalPreparationTime,
    Dictionary<string, string> PreparationSteps,
    Dictionary<string, string> IngredientAmounts);
