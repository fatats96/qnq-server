using Microsoft.AspNetCore.Mvc;
using QandQ.Core.Models;
using QandQ.Core.Repositories;

namespace QandQ.MovieAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FavoriteController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public FavoriteController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<OkResult> PostAddFavorite([FromBody] Favorite favorite)
        {
            await _unitOfWork.FavoriteRepository.AddAsync(favorite);
            await _unitOfWork.CommitAsync();
            return Ok();
        }
    }
}
