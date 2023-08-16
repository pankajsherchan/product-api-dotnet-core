using System.ComponentModel.DataAnnotations.Schema;

namespace ps_product_api.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime AddedDate { get; set; }

        // relationship with User

        [ForeignKey("UserId")]
        public int UserId { get; set; } // Required foreign key property
        public User User { get; set; } = null!; // Required reference navigation to principal

        // public User? User { get; set; } // Optional reference navigation to principal
    }
}