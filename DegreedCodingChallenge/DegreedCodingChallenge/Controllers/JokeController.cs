using DegreedCodingChallenge.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DegreedCodingChallenge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JokeController : ControllerBase
    {
        private readonly ILogger<JokeController> _logger;
        private readonly IJokeService _jokeService;

        public JokeController(ILogger<JokeController> logger, IJokeService jokeService)
        {
            _logger = logger;
            _jokeService = jokeService;
        }

        [HttpGet]
        [Route("")]
        public async Task<string> Get()
        {
            return await _jokeService.GetRandomJoke();
        }

        [HttpGet]
        [Route("{searchTerm}")]
        public async Task<Dictionary<string, List<string>>> Search(string searchTerm)
        {
            return await _jokeService.SearchJokes(searchTerm);
        }
    }
}
