using Business.DTOs;
using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces;

public interface IRecipeService
{
    Task<RecipeResponse> GetResponseByIdAsync(Guid id);
    Task<RecipeResponse> CreateAsync(UpsertRecipeRequest request);
    Task<RecipeResponse> UpdateAsync(Guid id, UpsertRecipeRequest request);
}
