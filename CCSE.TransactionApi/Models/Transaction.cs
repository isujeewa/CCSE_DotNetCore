namespace CCSE.TransactionApi.Models
{
    public class Transaction
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string StockID { get; set; }
        public string UserID{ get; set; }
        public string Type { get; set; }
        public float Qty { get; set; }
        public float Price { get; set; }

    }
}
