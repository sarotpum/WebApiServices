using AutoMapper;
using SharedService.Models.Characters.Dtos;
using SharedService.Models.Tlous;

namespace Dotnet7
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CharacterModel, GetCharacterDto>();
            CreateMap<AddCharacterDto, CharacterModel>();
            CreateMap<UpdateCharacterDto, CharacterModel>();
        }
    }
}