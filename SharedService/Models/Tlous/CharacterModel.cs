namespace SharedService.Models.Tlous
{
    public class CharacterModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public BackpackModel Backpack { get; set; } = new BackpackModel();
        public List<WeaponModel> Weapons { get; set; } = new List<WeaponModel>();
        public List<FactionModel> Factions { get; set; } = new List<FactionModel>();
    }
}
