using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SharedService.DBContext;
using SharedService.Models.Characters;
using SharedService.Models.Characters.Dtos;
using WebApiServices.Services.Interface;

namespace WebApiServices.Services.Implement
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper;
        private readonly DatasContext _datasContext;

        public CharacterService(IMapper mapper, DatasContext datasContext)
        {
            _mapper = mapper;
            _datasContext = datasContext;
        }

        private static List<CharacterModel> characters = new List<CharacterModel>
        {
            new CharacterModel(),
            new CharacterModel() {Name = "Sam"}
        };

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var dbCharacters = await _datasContext.Characters.ToListAsync();
            serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            var dbCharacter = await _datasContext.Characters.FirstOrDefaultAsync(c => c.Id == id);
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacter);
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var character = _mapper.Map<CharacterModel>(newCharacter);
            character.Id = characters.Max(x => x.Id) + 1;
            characters.Add(character);
            characters.Add(_mapper.Map<CharacterModel>(newCharacter));
            serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }
         
        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updateCharacter)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();

            try
            {
                var character = characters.FirstOrDefault(c => c.Id == updateCharacter.Id);

                if (character is null)
                    throw new Exception($"Character with Id  '{updateCharacter.Id}' not found. ");

                _mapper.Map(updateCharacter, character);

                character.Name = updateCharacter.Name;
                character.HitPoints = updateCharacter.HitPoints;
                character.Strength = updateCharacter.Strength;
                character.Defense = updateCharacter.Defense;
                character.Intelligence = updateCharacter.Intelligence;
                character.Class = updateCharacter.Class;

                serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return  serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacters(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();

            try
            {
                var character = characters.FirstOrDefault(c => c.Id == id);

                if (character is null)
                    throw new Exception($"Character with Id  '{id}' not found. ");

                characters.Remove(character);

                serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
    }
}
