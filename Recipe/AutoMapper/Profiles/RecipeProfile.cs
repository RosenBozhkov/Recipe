using Business.DTOs;
using AutoMapper;

namespace Recipe.AutoMapper.Profiles;

internal class RecipeProfile : Profile
{
    public RecipeProfile()
    {
        CreateMap<Persistence.Entities.Recipe, RecipeResponse>().PreserveReferences();
        CreateMap<RecipeResponse, Persistence.Entities.Recipe>().PreserveReferences();
    }
}
