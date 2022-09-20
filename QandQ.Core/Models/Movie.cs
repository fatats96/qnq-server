using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QandQ.Core.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Adult { get; set; }
        public string Overview { get; set; }
        public string ReleaseDate { get; set; }
        public string OriginalTitle { get; set; }
        public string OriginalLanguage { get; set; }
        public string PosterPath { get; set; }
        public int MovieId { get; set; }

        public IEnumerable<Genre> Genres { get; set; }
        public IEnumerable<Favorite> Favourites { get; set; }
    }
}
