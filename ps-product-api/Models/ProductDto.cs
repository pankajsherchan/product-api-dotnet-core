namespace ps_product_api.Models
{
    public class ProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime AddedDate { get; set; }
        public int UserId { get; set; }
    }
}