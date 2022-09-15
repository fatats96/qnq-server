using QandQ.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QandQ.Core.Repositories
{
    public interface IAuthRepository : IRepository<User>
    {
        Task<User> LoginAsync(User credentials);

        Task<User> GetByIdAsync(int id);
    }
}
