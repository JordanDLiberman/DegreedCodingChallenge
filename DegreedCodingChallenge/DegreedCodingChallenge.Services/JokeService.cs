﻿using DegreedCodingChallenge.Interfaces;
using DegreedCodingChallenge.Interfaces.Clients;
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
        public JokeService(ICanHazDadJokeClient client)
        {
            _client = client;
        }

        public async Task<string> GetRandomJoke()
        {
            return await _client.GetRandomJoke();
        }

        public async Task<Dictionary<string, List<string>>> SearchJokes(string searchTerm)
        {
            var jokes = await _client.SearchJokes(searchTerm);

            var response = new Dictionary<string, List<string>>();
            foreach(var j in jokes)
            {
                var highlighted = Regex.Replace(j, searchTerm, "||" + searchTerm + "||", RegexOptions.IgnoreCase);
                var size = GetJokeSize(j);
                if (response.ContainsKey(size))
                {
                    response[size].Add(highlighted);
                }
                else
                {
                    response.Add(size, new List<string>() { highlighted });
                }
            }
            return response;
        }

        private string GetJokeSize(string input)
        {
            var numberOfWords = NumberOfWordsInString(input);

            if (numberOfWords < 10)
            {
                return "Short";
            }
            else if (numberOfWords < 20)
            {
                return "Medium";
            }
            else 
            {
                return "Long";
            }
        }

        private int NumberOfWordsInString(string input)
        {
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