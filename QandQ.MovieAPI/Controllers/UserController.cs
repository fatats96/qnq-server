using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QandQ.Core.Models;
using QandQ.Core.Repositories;

namespace QandQ.MovieAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUnitOfWork unitOfWork)
        {
            _userRepository = unitOfWork.UserRepository;
        }

        [HttpGet("GetFavorites")]
        public async Task<IEnumerable<Favorite>> GetFavorites([FromQuery] int userId) 
            => await _userRepository.GetFavoriteAsync(userId);
    }
}
