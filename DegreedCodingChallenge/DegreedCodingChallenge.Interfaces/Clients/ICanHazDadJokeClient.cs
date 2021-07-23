using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DegreedCodingChallenge.Interfaces.Clients
{
    public interface ICanHazDadJokeClient
    {
        Task<string> GetRandomJoke();

        Task<List<string>> SearchJokes(string searchTerm);
    }
}
