using QandQ.Core.Repositories;
using QandQ.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QandQ.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MovieContext _context;

        private IAuthRepository _authRepository;
        private IGenreRepository _genreRepository;
        private IMovieRepository _movieRepository;
        private IFavoriteRepository _favoriteRepository;
        private IUserRepository _userRepository;

        public UnitOfWork(MovieContext context)
        {
            this._context = context;
        }

        public IAuthRepository Auth => _authRepository ??= new AuthRepository(_context);
        public IGenreRepository GenreRepository => _genreRepository ??= new GenreRepository(_context);
        public IMovieRepository MovieRepository => _movieRepository ??= new MovieRepository(_context);
        public IFavoriteRepository FavoriteRepository => _favoriteRepository ??= new FavoriteRepository(_context);
        public IUserRepository UserRepository => _userRepository ??= new UserRepository(_context);


        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
