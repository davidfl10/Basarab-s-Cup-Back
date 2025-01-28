namespace finalexam_back.Models
{
    public class Product
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Images { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public virtual ICollection<Cart>? Carts { get; set; }
    }
}
