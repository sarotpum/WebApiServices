namespace SharedService.Models.MasterDetailsOrders
{
    public class OrdersDetailsModel
    {
        public OrdersDetailsModel()
        {
            OrderItems = new List<OrderItems>();
            FormData = new FormData();
        }

        public List<OrderItems> OrderItems { get; set; }
        public FormData FormData { get; set; }
    }

    public class FormData
    {
        public int OrderID { get; set; }
        public string OrderNo { get; set; } = string.Empty;
        public int CustomerID { get; set; }
        public string PMethod { get; set; } = string.Empty;
        public double GTotal { get; set; }
    }

    public class OrderItems
    {
        public int OrderItemID { get; set; }
        public int OrderID { get; set; }
        public int ItemID { get; set; }
        public int Quantity { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public double Price { get; set; }
        public double Total { get; set; }
    }
}
