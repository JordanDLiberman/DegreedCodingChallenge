using DegreedCodingChallenge.Entities;
using DegreedCodingChallenge.Interfaces.Clients;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _config;
        private readonly string _baseUrl;
        public CanHazDadJokeClient(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
            _baseUrl = _config["ICanHazDadJokeUrl"];
            _httpClient.BaseAddress = new Uri(_baseUrl);
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Jordan Liberman Degreed coding challenge");
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }
        public async Task<string> GetRandomJoke()
        {
            var res = await _httpClient.GetAsync(_baseUrl);
            var jokeJson = await res.Content.ReadAsStringAsync();
            var joke = Newtonsoft.Json.JsonConvert.DeserializeObject<JokeDTO>(jokeJson);
            return joke.Joke;
        }

        public async Task<List<string>> SearchJokes(string searchTerm)
        {
            var res = await _httpClient.GetAsync($"{_baseUrl}search?page={_config["JokePage"]}&limit={_config["JokeLimit"]}&term=" + searchTerm);
            var jokeJson = await res.Content.ReadAsStringAsync();
            var jokes = Newtonsoft.Json.JsonConvert.DeserializeObject<JokeSearchResponseDTO>(jokeJson);
            return jokes.Results.Select(j => j.Joke).ToList();
        }
    }
}
