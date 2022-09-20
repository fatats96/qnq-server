using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QandQ.Core.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public int GenreId { get; set; }
        public string Name { get; set; }

        public IEnumerable<Movie> Movies { get; set; }
    }
}
