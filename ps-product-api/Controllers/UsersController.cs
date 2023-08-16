using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ps_product_api.Entities;
using ps_product_api.Models;
using ps_product_api.Services;

namespace ps_product_api.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var result = await _userRepository.GetUsers();
            return Ok(_mapper.Map<IEnumerable<UserDto>>(result));
        }

        [HttpGet("{id}", Name = "GetUser")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            var result = await _userRepository.GetUser(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<UserDto>(result));
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> AddUser(UserDto user)
        {
            var userToAdd = _mapper.Map<User>(user);

            await _userRepository.AddUser(userToAdd);

            var userFromDatabase = _mapper.Map<User>(userToAdd);
            return CreatedAtAction("GetUser", new { id = userToAdd.Id }, userFromDatabase);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(int id, UserDto user)
        {
            var userToUpdate = await _userRepository.GetUser(id);
            if (userToUpdate == null) return NotFound();

            // var userToUpdate = _mapper.Map<User>(user);
            // userToUpdate.Id = id;

            _mapper.Map(user, userToUpdate);

            await _userRepository.UpdateUser(userToUpdate);

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }

            await _userRepository.DeleteUser(user);
            return NoContent();
        }
    }
}