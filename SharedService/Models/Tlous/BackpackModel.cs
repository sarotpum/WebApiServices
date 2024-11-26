using System.Text.Json.Serialization;

namespace SharedService.Models.Tlous
{
    public class BackpackModel
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public int CharacterId { get; set; }
        [JsonIgnore]
        public CharacterModel Character { get; set; } = new CharacterModel();
    }
}
