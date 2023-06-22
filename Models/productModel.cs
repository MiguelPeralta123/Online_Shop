namespace OnlineShop.Models
{
    public class productModel
    {
        public productModel(string name, string description, decimal price)
        {
            this.name = name;
            this.description = description;
            this.price = price;
        }
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
    }
}
