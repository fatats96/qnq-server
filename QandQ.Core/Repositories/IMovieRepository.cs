using QandQ.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QandQ.Core.Repositories
{
    public interface IMovieRepository : IRepository<Movie>
    {
        Task<IEnumerable<Movie>> GetByPagination(Pagination pagination);
        Task<IEnumerable<Movie>> SearchAsync(string movieName);
        Task<IEnumerable<string>> GetMovieNamesAsync(string movieName);
        Task<Movie?> GetMovieWithGenresAsync(int id);
    }
}
