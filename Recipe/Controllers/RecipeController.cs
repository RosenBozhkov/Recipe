using BuberBreakfast.Controllers;
using Business.DTOs;
using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Recipe.Controllers;

[Route("[controller]")]
public class RecipeController : ApiController
{
    private static readonly string ControllerName = typeof(RecipeController).FullName!;
    private readonly IRecipeService _recipeService;

    public RecipeController(IRecipeService recipeService)
    {
        _recipeService = recipeService;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        try
        {
            RecipeResponse recipeResponse = await _recipeService.GetResponseByIdAsync(id);

            return Ok(recipeResponse);
        }
        catch (Exception)
        {
            throw new Exception("TILT");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(UpsertRecipeRequest request)
    {
        RecipeResponse recipeResponse = await _recipeService.CreateAsync(request);

        return Ok(recipeResponse);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(Guid id, UpsertRecipeRequest request)
    {
        RecipeResponse recipeResponse = await _recipeService.UpdateAsync(id, request);

        return Ok(recipeResponse);
    }
}