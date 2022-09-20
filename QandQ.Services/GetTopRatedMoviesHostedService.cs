using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using QandQ.Core.Models;
using QandQ.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QandQ.Services
{
    public class GetTopRatedMoviesHostedService : IHostedService
    {
        HttpClient HttpClient = new HttpClient();

        private Timer _timer;
        private readonly int nextWorkTime = 24 * 60 * 60 * 1000 * 7;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public GetTopRatedMoviesHostedService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            string apiResponse = null;
            dynamic list = null;
            var scope = _serviceScopeFactory.CreateScope();
            var unitOfWorkService = scope.ServiceProvider.GetService<IUnitOfWork>();
            var movieRepo = unitOfWorkService.MovieRepository;
            var genreRepo = unitOfWorkService.GenreRepository;
            int page = 1;
            while (!cancellationToken.IsCancellationRequested)
            {
                while (page <= 500)
                {
                    HttpResponseMessage response = await HttpClient.GetAsync($"https://api.themoviedb.org/3/movie/popular?api_key=b7f4f7f7546bf9a6f8ef3baa1299f064&page={page}");
                    apiResponse = await response.Content.ReadAsStringAsync();
                    list = JsonConvert.DeserializeObject<dynamic>(apiResponse);
                    List<Movie> movies = new();
                    foreach (var movie in list.results)
                    {
                        var title = (string)movie.title;
                        var _movie = await movieRepo.SingleOrDefaultAsync(x => x.Title == title);
                        if (_movie == null)
                        {
                            List<Genre> genres = new List<Genre>();
                            foreach (var itemGenre in movie.genre_ids)
                            {
                                int genreId = (int)itemGenre;
                                Genre genre = await genreRepo.SingleOrDefaultAsync(predicate: x => x.GenreId == genreId);
                                genres.Add(genre);
                            }
                            movies.Add(new Movie
                            {
                                Adult = movie.adult,
                                Genres = genres,
                                OriginalLanguage = movie.original_language,
                                OriginalTitle = movie.original_title,
                                Overview = movie.overview,
                                PosterPath = movie.poster_path,
                                ReleaseDate = movie.release_date,
                                Title = movie.title,
                                MovieId = movie.id
                            });
                        }
                    }
                    await movieRepo.AddRangeAsync(movies);
                    await unitOfWorkService.CommitAsync();
                    Console.WriteLine("Movie Count Written to Db: " + movies.Count + " Page: " +  page);
                    page++;
                    movies.Clear();
                }   
                await Task.Delay(nextWorkTime, cancellationToken);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
    }
}
