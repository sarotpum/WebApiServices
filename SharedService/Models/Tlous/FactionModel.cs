namespace SharedService.Models.Tlous
{
    public class FactionModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<CharacterModel> Characters { get; set; } = new List<CharacterModel>();
    }
}
