using AutoMapper;
using Business.DTOs;
using Persistence.Entities;

namespace Recipe.AutoMapper.Profiles;

public class PreparationStepProfile : Profile
{
    public PreparationStepProfile()
    {
        CreateMap<PreparationStep, PreparationStepResponse>().PreserveReferences();
        CreateMap<PreparationStepResponse, PreparationStep>().PreserveReferences();
    }
}
