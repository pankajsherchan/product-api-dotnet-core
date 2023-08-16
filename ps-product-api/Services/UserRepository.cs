using ps_product_api.Contexts;
using ps_product_api.Entities;
using Microsoft.EntityFrameworkCore;


namespace ps_product_api.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly ProductContext _productContext;
        public UserRepository(ProductContext productContext)
        {
            _productContext = productContext ?? throw new ArgumentNullException(nameof(productContext));
        }

        public async Task<User> GetUser(int id)
        {
            return await _productContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _productContext.Users.ToListAsync();
        }

        public async Task AddUser(User user)
        {
            _productContext.Users.Add(user);
            await _productContext.SaveChangesAsync();
        }

        public async Task UpdateUser(User user)
        {
            // _productContext.Users.Update(user);
            _productContext.Users.Update(user);
            await _productContext.SaveChangesAsync();
        }

        public async Task DeleteUser(User user)
        {
            _productContext.Users.Remove(user);
            await _productContext.SaveChangesAsync();
        }
    }
}