using ps_product_api.Contexts;
using ps_product_api.Entities;
using Microsoft.EntityFrameworkCore;

namespace ps_product_api.Services
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext _context;

        public ProductRepository(ProductContext productContext)
        {
            _context = productContext ?? throw new ArgumentNullException(nameof(ProductContext));
        }
        public async Task<Product> GetProduct(int userId, int productId)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.UserId == userId && p.Id == productId);
        }

        public async Task<IEnumerable<Product>> GetProducts(int userId)
        {
            return await _context.Products.Where(p => p.UserId == userId).Include(p => p.User).ToListAsync();
        }

        public IAsyncEnumerable<Product> GetProductsAsAsyncEnumerable(int userId)
        {
            return _context.Products.Where(p => p.UserId == userId).Include(p => p.User).AsAsyncEnumerable<Product>();
        }

        public async Task Addproduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task AddProducts(IEnumerable<Product> products)
        {
            foreach (var product in products)
            {
                _context.Products.Add(product);
            }

            await _context.SaveChangesAsync();
        }
    }
}