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
        Task<int> CommitAsync();
    }
}
