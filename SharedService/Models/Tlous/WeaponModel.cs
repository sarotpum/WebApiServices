using System.Text.Json.Serialization;

namespace SharedService.Models.Tlous
{
    public class WeaponModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CharacterId { get; set; }
        [JsonIgnore]
        public CharacterModel Character { get; set; } = new CharacterModel();
    }
}
