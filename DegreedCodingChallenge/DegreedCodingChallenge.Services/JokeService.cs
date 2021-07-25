using DegreedCodingChallenge.Entities;
using DegreedCodingChallenge.Interfaces;
using DegreedCodingChallenge.Interfaces.Clients;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DegreedCodingChallenge.Services
{
    public class JokeService : IJokeService
    {
        private readonly ICanHazDadJokeClient _client;
        private readonly IConfiguration _config;
        public JokeService(ICanHazDadJokeClient client, IConfiguration config)
        {
            _client = client;
            _config = config;
        }

        public async Task<string> GetRandomJoke()
        {
            return await _client.GetRandomJoke();
        }

        public async Task<Dictionary<string, List<string>>> SearchJokes(string searchTerm)
        {
            var jokes = await _client.SearchJokes(searchTerm);

            var dict = jokes.Select(j => Regex.Replace(j, searchTerm, _config["JokeSearchTermHighlight"] + searchTerm + _config["JokeSearchTermHighlight"], RegexOptions.IgnoreCase))
                .GroupBy(j => GetJokeSize(j))
                .ToDictionary(j => j.Key, j => j.ToList());
            return dict;

        }

        private string GetJokeSize(string input)
        {
            var numberOfWords = NumberOfWordsInString(input);

            if (numberOfWords < 10)
            {
                return JokeLength.Short.ToString();
            }
            else if (numberOfWords < 20)
            {
                return JokeLength.Medium.ToString();
            }
            else 
            {
                return JokeLength.Long.ToString();
            }
        }

        private int NumberOfWordsInString(string input)
        {
            //This could also be implemented by doing a string.Split(' ').Count(), which would use more memory but probably be more performant.
            var count = 0;
            foreach(var c in input)
            {
                if (c == ' ' || c == '\r' || c == '\n')
                {
                    count++;
                }
            }
            return count;
        }
    }
}
