using AutoMapper;
using Business.DTOs;
using Business.Interfaces;
using Business.ServiceErrors;
using ErrorOr;
using Persistence.Entities;
using Persistence.Interfaces;

namespace Business.Implementations;

public class RecipeService : IRecipeService
{
    private readonly IIngredientService _ingredientService;
    private readonly IRecipeRepository _recipeRepository;
    private readonly IMapper _mapper;

    public RecipeService(IRecipeRepository recipeRepository, IIngredientService ingredientService, IMapper mapper)
    {
        _recipeRepository = recipeRepository;
        _ingredientService = ingredientService;
        _mapper = mapper;
    }

    public async Task<RecipeResponse> GetResponseByIdAsync(Guid id)
    {
        Recipe? requestToRecipeResult = await GetByIdAsync(id);

        RecipeResponse result = _mapper.Map<RecipeResponse>(requestToRecipeResult);
        return result;
    }

    public async Task<RecipeResponse> CreateAsync(UpsertRecipeRequest request)
    {
        TimeSpan prepTime = ValidateAndParseTime(request.TotalPreparationTime);
        await ValidateRecipeDoesNotExist(request);
        IList<IngredientAmount> ingredientAmounts = await ValidateAndMapIngredientAmounts(request.IngredientAmounts);
        IList<PreparationStep> preparationSteps = ValidateAndMapPreparationSteps(request.PreparationSteps);
        ValidateTotalPreparationTime(prepTime, preparationSteps);

        Recipe recipe = new()
        {
            Name = request.Name,
            MealType = request.MealType,
            Difficulty = request.Difficulty,
            Description = request.Description,
            PreparationSteps = preparationSteps,
            TotalPreparationTime = prepTime,
            IngredientAmounts = ingredientAmounts
        };

        Recipe newRecipe = _recipeRepository.Add(recipe);
        await _recipeRepository.SaveChangesAsync();

        RecipeResponse response = _mapper.Map<RecipeResponse>(newRecipe);
        return response;
    }

    public async Task<RecipeResponse> UpdateAsync(Guid id, UpsertRecipeRequest request)
    {
        Recipe? recipeToUpdate = await GetByIdAsync(id);

        TimeSpan prepTime = ValidateAndParseTime(request.TotalPreparationTime);

        //TODO: For now this feels bad (prepSteps and totalTime).
        IList<PreparationStep> preparationSteps = ValidateAndMapPreparationSteps(request.PreparationSteps);
        ValidateTotalPreparationTime(prepTime, preparationSteps);

        recipeToUpdate.Name = request.Name;
        recipeToUpdate.MealType = request.MealType;
        recipeToUpdate.Difficulty = request.Difficulty;
        recipeToUpdate.Description = request.Description;
        recipeToUpdate.PreparationSteps = preparationSteps;
        recipeToUpdate.TotalPreparationTime = prepTime;

        await _recipeRepository.SaveChangesAsync();

        RecipeResponse response = _mapper.Map<RecipeResponse>(recipeToUpdate);
        return response;
    }

    private async Task<IList<IngredientAmount>> ValidateAndMapIngredientAmounts(Dictionary<string, string> requestIngredientAmounts)
    {
        IList<IngredientAmount> ingredientAmounts = new List<IngredientAmount>();

        foreach (KeyValuePair<string, string> amountUnitPair in requestIngredientAmounts)
        {
            Ingredient ingredient = await _ingredientService.GetOrCreateAsync(amountUnitPair.Key);
            
            IngredientAmount ingredientAmount = ParseIngredientAmount(amountUnitPair, ingredient);

            ingredientAmounts.Add(ingredientAmount);
        }

        return ingredientAmounts;
    }

    private static IngredientAmount ParseIngredientAmount(KeyValuePair<string, string> amountUnitPair, Ingredient ingredient)
    {
        string[] amountAndUnit = amountUnitPair.Value.Split(" ");

        bool isCorrectLength = amountAndUnit.Length == 2;

        bool isCorretDecimal = Decimal.TryParse(amountAndUnit[0], out decimal amount);

        bool toTaste = !isCorretDecimal || !isCorrectLength;

        if (toTaste)
        {
            return new IngredientAmount() { Ingredient = ingredient, Unit = "to taste" }; 
        }

        return new IngredientAmount() { Ingredient = ingredient, Amount = amount, Unit = amountAndUnit[1] };
    }

    private static IList<PreparationStep> ValidateAndMapPreparationSteps(Dictionary<string, string> requestPreparationSteps)
    {
        IList<PreparationStep> preparationSteps = new List<PreparationStep>();
        int stepNumber = 0;

        foreach (KeyValuePair<string, string> descriptionDurationPair in requestPreparationSteps)
        {
            PreparationStep prepStep = new()
            {
                Duration = ValidateAndParseTime(descriptionDurationPair.Value),
                Description = descriptionDurationPair.Key,
                Number = ++stepNumber
            };

            preparationSteps.Add(prepStep);
        }

        return preparationSteps;
    }

    private static void ValidateTotalPreparationTime(TimeSpan totalTime, IList<PreparationStep> preparationSteps)
    {
        var preparationStepsDuration = preparationSteps.Sum(ps => ps.Duration.TotalHours);

        if (preparationStepsDuration * 1.2 <= totalTime.TotalHours)
        {
            throw new ArgumentException("Not good enough preparation steps. Be more specific.");
        }
    }

    private static TimeSpan ValidateAndParseTime(string timeSpan)
    {
        bool isCorretTimeSpan = TimeSpan.TryParse(timeSpan, out var prepTime);

        if (!isCorretTimeSpan)
        {
            throw new ArgumentException("Invalid Preparation time.");
        }

        return prepTime;
    }


    private async Task ValidateRecipeDoesNotExist(UpsertRecipeRequest request)
    {
        Recipe? recipe = await _recipeRepository.GetByNameAsync(request.Name);

        if (recipe is not null && recipe.Description == request.Description)
        {
            throw new ArgumentException("ResourceAlreadyExists");
        }
    }

    private async Task<Recipe> GetByIdAsync(Guid id)
    {
        return await _recipeRepository.GetByIdAsync(id) ?? throw new ArgumentException("Not Found");
    }
}
