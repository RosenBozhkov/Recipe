using AutoMapper;
using Business.Interfaces;
using Persistence.Entities;
using Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Implementations;

public class IngredientService : IIngredientService
{
    private readonly IIngredientRepository _ingredientRepository;
    private readonly IMapper _mapper;

    public IngredientService(IIngredientRepository ingredientRepository, IMapper mapper)
    {
        _ingredientRepository = ingredientRepository;
        _mapper = mapper;
    }

    // NOTE: this method is designed to be used only from RecipeService thats why i don't save changes
    public async Task<Ingredient> GetOrCreateAsync(string name)
    {
        Ingredient? ingredient = await _ingredientRepository.GetByNameAsync(name);

        if (ingredient is not null)
        {
            return ingredient;
        }

        Ingredient newIngredient = _ingredientRepository.Add(new Ingredient { Name = name } );
        return newIngredient;
    }
}
