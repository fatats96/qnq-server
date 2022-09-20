using Microsoft.EntityFrameworkCore;
using QandQ.Core;
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
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        public MovieRepository(DbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Movie>> SearchAsync(string movieName)
        {
            return await MovieContext
                .Movies
                .Include(x => x.Genres)
                .Where(x => x.Title.ToLower()
                    .Contains(movieName.ToLower())
                 )
                .Select(m =>
                new Movie
                {
                    Id = m.Id,
                    MovieId = m.MovieId,
                    Title = m.Title,
                    PosterPath = m.PosterPath,
                    ReleaseDate = m.ReleaseDate,
                    Genres = m.Genres.ToList()
                })
                .Take(2)
                .ToListAsync();
        }

        public async Task<IEnumerable<string>> GetMovieNamesAsync(string movieName)
        {
            return await MovieContext
                .Movies
                .Where(x => x.Title.ToLower()
                    .Contains(movieName.ToLower())
                 )
                .Take(10)
                .Select(x => x.Title)
                .ToListAsync();
        }

        public async Task<IEnumerable<Movie>> GetByPagination(Pagination pagination)
        {
            return await MovieContext.Movies.Skip((pagination.Page - 1) * pagination.Count).Take(pagination.Count).ToListAsync();
        }

        public async Task<Movie?> GetMovieWithGenresAsync(int id) => await MovieContext.Movies.Include(x => x.Genres).Select(m => new Movie
        {
            Id = m.Id,
            MovieId = m.MovieId,
            Title = m.Title,
            PosterPath = m.PosterPath,
            ReleaseDate = m.ReleaseDate,
            Adult = m.Adult,
            OriginalLanguage = m.OriginalLanguage,
            OriginalTitle = m.OriginalTitle,
            Overview = m.Overview,
            Genres = m.Genres.ToList()
        }).FirstOrDefaultAsync(x => x.Id == id);

        private MovieContext MovieContext
        {
            get { return Context as MovieContext; }
        }
    }
}
