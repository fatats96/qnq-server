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
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Favorite>> GetFavoriteAsync(int userId)
        {
            return await MovieContext
                .Favorites
                .Include(x => x.Movie)
                .Where(x => x.UserId == userId)
                .Select(x => new Favorite
                {
                    Id = x.Id,
                    Movie = new Movie
                    {
                        Id = x.Id,
                        MovieId = x.MovieId,
                        Title = x.Movie.Title,
                        PosterPath = x.Movie.PosterPath,
                        ReleaseDate = x.Movie.ReleaseDate,
                    }
                })
                .ToListAsync();
        }

        public MovieContext MovieContext
        {
            get { return Context as MovieContext; }
        }
    }
}
