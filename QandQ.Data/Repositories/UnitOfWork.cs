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

        public UnitOfWork(MovieContext context)
        {
            this._context = context;
        }

        public IAuthRepository Auth => _authRepository ??= new AuthRepository(_context);

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
