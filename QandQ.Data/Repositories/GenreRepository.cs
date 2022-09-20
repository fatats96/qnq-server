using Microsoft.EntityFrameworkCore;
using QandQ.Core.Models;
using QandQ.Core.Repositories;
using QandQ.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QandQ.Data.Repositories
{
    public class GenreRepository : Repository<Genre>, IGenreRepository
    {
        public GenreRepository(DbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Genre>> GetGenresWithMoviesAsync()
        {
            return await MovieContext.Genres.Include(x => x.Movies).Select(x => new Genre
            {
                Id = x.Id,
                GenreId = x.GenreId,
                Name = x.Name,
                Movies = x.Movies.Select(m => new Movie
                {
                    Id = m.Id,
                    Genres = new List<Genre>(),
                    PosterPath = m.PosterPath,
                    ReleaseDate = m.ReleaseDate,
                    Title = m.Title,
                }).Take(6).ToList()
            }).ToListAsync();
        }

        private MovieContext MovieContext
        {
            get { return Context as MovieContext; }
        }

    }
}
