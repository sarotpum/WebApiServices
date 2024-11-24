using System.ComponentModel.DataAnnotations;

namespace SharedService.Models.Card
{
    public class CardModel
    {
        [Key]
        public Guid Id { get; set; }
        public string CardholderName { get; set; } = string.Empty;
        public string CardNumber { get; set; } = string.Empty;
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public int CVC { get; set; }
    }
}
