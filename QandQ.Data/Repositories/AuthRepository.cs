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
    public class AuthRepository : Repository<User>, IAuthRepository
    {
        public AuthRepository(MovieContext context)
            : base(context)
        {

        }

        public async Task<User> LoginAsync(User credentials)
        {
            var user = await MovieContext.Users.FirstOrDefaultAsync(x => x.Email == credentials.Email && x.Password == credentials.Password);
            if (user!=null)
            {
                user.Password = String.Empty;

                return user;
            }
            return null;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await MovieContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        private MovieContext MovieContext
        {
            get { return Context as MovieContext; }
        }
    }
}
