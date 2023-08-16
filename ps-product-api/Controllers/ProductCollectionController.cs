using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ps_product_api.Entities;
using ps_product_api.Models;
using ps_product_api.Services;

namespace ps_product_api.Controllers
{
    [ApiController]
    [Route("users/{userId}/productcollections")]
    public class ProductCollectionController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public ProductCollectionController(IProductRepository productRepository, IUserRepository userRepository, IMapper mapper)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(IProductRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(IUserRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(IMapper));
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> AddUserProducts(int userId, IEnumerable<ProductDto> products)
        {
            var user = await _userRepository.GetUser(userId);

            if (user == null)
            {
                return NotFound();
            }

            foreach (var product in products)
            {
                product.UserId = userId;
            }

            var productsToAdd = _mapper.Map<IEnumerable<Product>>(products);

            await _productRepository.AddProducts(productsToAdd);

            return Ok();
        }
    }
}