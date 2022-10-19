using Persistence.Entities;
using Persistence.Entities.Enums;

namespace Business.DTOs;

public record RecipeResponse(
    Guid Id,
    string Name,
    string Description,
    string MealType,
    string Difficulty,
    TimeSpan TotalPreparationTime,
    List<IngredientAmountResponse> IngredientAmounts,
    List<PreparationStepResponse> PreparationSteps);