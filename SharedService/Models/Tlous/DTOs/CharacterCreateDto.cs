namespace SharedService.Models.Tlous.DTOs
{
    public record struct CharacterCreateDto(
        string Name,
        BackpackCreateDto Backpack,
        List<WeaponCreateDto> Weapons,
        List<FactionCreateDto> Factions);
}
