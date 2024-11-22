using System.ComponentModel.DataAnnotations;

namespace SharedService.Models.PaymentDetail
{
    public class PaymentDetailModel
    {
        [Key]
        public int PMId { get; set; }
        //[Required]
        //[Column(TypeName ="nvarchar(100)")]
        public string CardOwnerName { get; set; } = string.Empty;
        //[Required]
        //[Column(TypeName = "varchar(16)")]
        public string CardNumber { get; set; } = string.Empty;
        // [Required]
        // [Column(TypeName = "varchar(5)")]
        public string Expiration { get; set; } = string.Empty;
        // [Required]
        // [Column(TypeName = "varchar(3)")]
        public string CVV { get; set; } = string.Empty;
    }
}
