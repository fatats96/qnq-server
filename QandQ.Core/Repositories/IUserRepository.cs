using QandQ.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QandQ.Core.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<IEnumerable<Favorite>> GetFavoriteAsync(int userId);
    }
}
