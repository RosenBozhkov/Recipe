using AutoMapper;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Recipe.AutoMapper.Profiles;

namespace Recipe.AutoMapper;

public static class ConfigureAutoMapperServices
{
    public static IMapper ConfigureAutomapper(this IServiceCollection services)
    {
        var config = new MapperConfiguration(configuration =>
        {
            configuration.AddProfile(new RecipeProfile());
            configuration.AddProfile(new IngredientAmountProfile());
            configuration.AddProfile(new PreparationStepProfile());
        });

        var mapper = config.CreateMapper();
        services.TryAddSingleton(mapper);
        return mapper;
    }
}
