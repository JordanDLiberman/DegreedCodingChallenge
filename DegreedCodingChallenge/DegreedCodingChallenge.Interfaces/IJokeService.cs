using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DegreedCodingChallenge.Interfaces
{
    public interface IJokeService
    {
        Task<string> GetRandomJoke();

        Task<Dictionary<string, List<string>>> SearchJokes(string searchTerm);
    }
}
