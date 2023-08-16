using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ps_product_api.Entities;
using ps_product_api.Models;
using ps_product_api.Services;

namespace ps_product_api.Controllers
{
    [ApiController]
    [Route("users/{userId}/products")]
    public class ProductsController : ControllerBase
    {
        // GetProducts
        // GetProduct
        // addProduct
        // deleteProduct
        // updateProduct

        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository productRepository, IUserRepository userRepository, IMapper mapper)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(IProductRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(IUserRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(IMapper));
        }

        [HttpGet]
        public async Task<ActionResult<ProductDto>> GetProductsForUser(int userId)
        {
            var user = await _userRepository.GetUser(userId);

            if (user == null)
            {
                return NotFound();
            }

            var products = await _productRepository.GetProducts(userId);

            return Ok(_mapper.Map<ProductDto>(products));
        }

        [HttpGet("productstreams")]
        public async IAsyncEnumerable<ProductDto> GetProductStreams(int userId)
        {
            // var user = await _userRepository.GetUser(userId);

            // if (user == null)
            // {
            //     return NotFound();
            // }

            await foreach (var product in _productRepository.GetProductsAsAsyncEnumerable(userId))
            {
                await Task.Delay(500);
                yield return _mapper.Map<ProductDto>(product);
            }

            // return Ok(_mapper.Map<ProductDto>(products));
        }

        [HttpGet("{productId}", Name = "GetProductForUser")]
        public async Task<ActionResult<ProductDto>> GetProductForUser(int userId, int productId)
        {
            var user = await _userRepository.GetUser(userId);

            if (user == null)
            {
                return NotFound();
            }

            var product = await _productRepository.GetProduct(userId, productId);
            return Ok(_mapper.Map<ProductDto>(product));
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> AddUserProduct(int userId, ProductDto product)
        {
            var user = await _userRepository.GetUser(userId);

            if (user == null)
            {
                return NotFound();
            }

            product.UserId = userId;
            var productToAdd = _mapper.Map<Product>(product);

            await _productRepository.Addproduct(productToAdd);

            return NoContent();
        }


        [HttpPut("{productId}")]
        public async Task<ActionResult<ProductDto>> UpdateUserProduct(int userId, int productId, ProductDto product)
        {
            var user = await _userRepository.GetUser(userId);

            if (user == null)
            {
                return NotFound();
            }

            var productInDb = await _productRepository.GetProduct(userId, productId);

            if (productInDb == null)
            {
                return NotFound();
            }

            _mapper.Map(product, productInDb);

            await _productRepository.UpdateProduct(productInDb);

            return CreatedAtAction("GetProductForUser", new
            {
                productId = productInDb.Id
            }, productInDb);
        }


        [HttpPut("{productId}")]
        public async Task<ActionResult<ProductDto>> DeleteUserProduct(int userId, int productId)
        {
            var user = await _userRepository.GetUser(userId);

            if (user == null)
            {
                return NotFound();
            }

            var productInDb = await _productRepository.GetProduct(userId, productId);

            if (productInDb == null)
            {
                return NotFound();
            }

            await _productRepository.DeleteProduct(productInDb);

            return NoContent();
        }
    }
}