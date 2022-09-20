using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QandQ.Core.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IAuthRepository Auth { get; }
        IGenreRepository GenreRepository { get; }
        IMovieRepository MovieRepository { get; }
        IFavoriteRepository FavoriteRepository { get; }
        IUserRepository UserRepository { get; }

        Task<int> CommitAsync();
    }
}
