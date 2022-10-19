using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Entities;
using Persistence.Interfaces;

namespace Persistence.Implementations;

public class RecipeRepository : IRecipeRepository
{
    private readonly RecipeContext _context;

    public RecipeRepository(RecipeContext context)
    {
        _context = context;
    }

    public async Task<Recipe?> GetByIdAsync(Guid id)
    {
        return await _context.Recipes.Include(r => r.PreparationSteps).Include(r => r.IngredientAmounts).ThenInclude(ri => ri.Ingredient).FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<Recipe?> GetByNameAsync(string name)
    {
        return await _context.Recipes.FirstOrDefaultAsync(r => r.Name == name);
    }

    public Recipe Add(Recipe recipe)
    {
        return _context.Add(recipe).Entity;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
