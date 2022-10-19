using AutoMapper;
using Business.DTOs;
using Persistence.Entities;

namespace Recipe.AutoMapper.Profiles;

public class IngredientAmountProfile : Profile
{
    public IngredientAmountProfile()
    {
        CreateMap<IngredientAmount, IngredientAmountResponse>().PreserveReferences();
        CreateMap<IngredientAmountResponse, IngredientAmount>().PreserveReferences();
    }
}
