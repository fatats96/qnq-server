using Microsoft.AspNetCore.Mvc;
using QandQ.Core;
using QandQ.Core.Models;
using QandQ.Core.Repositories;

namespace QandQ.MovieAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;

        public MovieController(IUnitOfWork unitOfWork)
        {
            _movieRepository = unitOfWork.MovieRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Movie>> GetMovies([FromQuery] Pagination pagination) => await _movieRepository.GetByPagination(pagination);

        [HttpGet("GetMovie")]
        public async Task<Movie?> GetMovie([FromQuery] int id) => await _movieRepository.GetMovieWithGenresAsync(id);

        [HttpGet("GetSearchedMovies")]
        public async Task<IEnumerable<Movie>> GetSearchedMovies([FromQuery] string movieName)
        {
            return await _movieRepository.SearchAsync(movieName);
        }

        [HttpGet("GetAutoCompletedMovieNames")]
        public async Task<IEnumerable<string>> GetAutoCompletedMovieNames([FromQuery] string movieName)
        {
            return await _movieRepository.GetMovieNamesAsync(movieName);
        }
    }
}
