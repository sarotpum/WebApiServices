namespace SharedService.Models.Products
{
    public class ProductModel
    {
        public int id { get; set; }
        public string name { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public string price { get; set; } = string.Empty;
    }
}
