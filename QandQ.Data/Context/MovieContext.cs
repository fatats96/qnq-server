using Microsoft.EntityFrameworkCore;
using QandQ.Core.Models;
using QandQ.Data.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QandQ.Data.Context
{
    public class MovieContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public MovieContext(DbContextOptions<MovieContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
        }
    }
}
