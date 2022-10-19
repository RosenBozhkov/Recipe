using Persistence.Entities.Enums;

namespace Business.DTOs;

public record CreateRecipeRequest(
    string Name,
    string Description,
    MealType MealType,
    DifficultyLevel Difficulty,
    string TotalPreparationTime,
    Dictionary<string, string> PreparationSteps,
    Dictionary<string, string> IngredientAmounts);