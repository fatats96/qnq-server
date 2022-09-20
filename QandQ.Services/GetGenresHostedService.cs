using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using QandQ.Core.Models;
using QandQ.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace QandQ.Services
{
    internal class GenreObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    internal class GenreResponse
    {
        public IEnumerable<GenreObject> genres { get; set; }
    }

    public class GetGenresHostedService : IHostedService
    {
        HttpClient HttpClient = new HttpClient();
        
        private Timer _timer;
        private readonly int nextWorkTime = 24 * 60 * 60 * 1000 * 7;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public GetGenresHostedService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        private async Task<dynamic> GetGenresAsync()
        {
            dynamic apiResponse = null;
            GenreResponse genres = new();
            List<Genre> genreList = new();
            HttpResponseMessage response = await HttpClient.GetAsync("https://api.themoviedb.org/3/genre/movie/list?api_key=b7f4f7f7546bf9a6f8ef3baa1299f064");
            if (response.IsSuccessStatusCode)
            {
                var scope = _serviceScopeFactory.CreateScope();
                var unitOfWorkService = scope.ServiceProvider.GetService<IUnitOfWork>();
                var genreRepository = unitOfWorkService.GenreRepository;

                apiResponse = await response.Content.ReadAsStringAsync();
                genres = JsonConvert.DeserializeObject<GenreResponse>(apiResponse);
                foreach (var item in genres.genres)
                {
                    genreList.Add(new Genre { GenreId = item.Id, Name = item.Name });
                }
                genreRepository.AddRangeAsync(genreList);
                await unitOfWorkService.CommitAsync();
            }
            return null;
        } 

        void HelloFromService(object state)
        {
            Console.WriteLine("I m runningg");
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            while(!cancellationToken.IsCancellationRequested)
            {
                await GetGenresAsync();
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
