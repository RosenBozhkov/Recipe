using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Entities;
using Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Implementations;

public class IngredientRepository : IIngredientRepository
{
    private readonly RecipeContext _context;

    public IngredientRepository(RecipeContext context)
    {
        _context = context;
    }

    public async Task<Ingredient?> GetByIdAsync(Guid id)
    {
        return await _context.Ingredients.FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<Ingredient?> GetByNameAsync(string name)
    {
        return await _context.Ingredients.FirstOrDefaultAsync(r => r.Name == name);
    }

    public Ingredient Add(Ingredient ingredient)
    {
        return _context.Add(ingredient).Entity;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
