using Microsoft.EntityFrameworkCore;
using QandQ.Core.Models;
using QandQ.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QandQ.Data.Repositories
{
    public class FavoriteRepository : Repository<Favorite>, IFavoriteRepository
    {
        public FavoriteRepository(DbContext context) : base(context)
        {
        }
    }
}
