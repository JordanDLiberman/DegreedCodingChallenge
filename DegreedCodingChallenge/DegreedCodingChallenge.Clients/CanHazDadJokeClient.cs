using DegreedCodingChallenge.Entities;
using DegreedCodingChallenge.Interfaces.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DegreedCodingChallenge.Clients
{
    public class CanHazDadJokeClient : ICanHazDadJokeClient
    {
        private readonly HttpClient _httpClient;
        public CanHazDadJokeClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://icanhazdadjoke.com/");
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Jordan Liberman Degreed coding challenge");
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }
        public async Task<string> GetRandomJoke()
        {
            var res = await _httpClient.GetAsync("https://icanhazdadjoke.com/");
            var jokeJson = await res.Content.ReadAsStringAsync();
            var joke = Newtonsoft.Json.JsonConvert.DeserializeObject<JokeDTO>(jokeJson);
            return joke.Joke;
        }

        public async Task<List<string>> SearchJokes(string searchTerm)
        {
            var res = await _httpClient.GetAsync("https://icanhazdadjoke.com/search?page=1&limit=30&term=" + searchTerm);
            var jokeJson = await res.Content.ReadAsStringAsync();
            var jokes = Newtonsoft.Json.JsonConvert.DeserializeObject<JokeSearchResponseDTO>(jokeJson);
            return jokes.Results.Select(j => j.Joke).ToList();
        }
    }
}
