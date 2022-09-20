using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using QandQ.Core.Models;
using QandQ.Core.Repositories;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace QandQ.MovieAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class GenreController : ControllerBase
    {
        private readonly IGenreRepository _genreRepository;
        public GenreController(IUnitOfWork unitOfWork)
        {
            _genreRepository = unitOfWork.GenreRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Genre>> GetGenres() => await _genreRepository.GetAllAsync();

        [HttpGet("GenresWithMovies")]
        public async Task<IEnumerable<Genre>> GetGenresWithMovies() =>
             await _genreRepository.GetGenresWithMoviesAsync();



    }
}
