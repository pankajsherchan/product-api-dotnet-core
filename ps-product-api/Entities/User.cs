namespace ps_product_api.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        // relationship with product
        public ICollection<Product> Products { get; } = new List<Product>();

    }
}