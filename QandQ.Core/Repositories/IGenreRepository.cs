using QandQ.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QandQ.Core.Repositories
{
    public interface IGenreRepository : IRepository<Genre>
    {   
        Task<IEnumerable<Genre>> GetGenresWithMoviesAsync();

    }
}
