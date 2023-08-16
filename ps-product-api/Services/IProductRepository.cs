using ps_product_api.Entities;

namespace ps_product_api.Services
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts(int userId);
        IAsyncEnumerable<Product> GetProductsAsAsyncEnumerable(int userId);
        Task<Product> GetProduct(int userId, int productId);
        Task Addproduct(Product product);
        Task UpdateProduct(Product product);
        Task DeleteProduct(Product product);
        Task AddProducts(IEnumerable<Product> products);
    }
}