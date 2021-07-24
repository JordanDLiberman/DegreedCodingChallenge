using DegreedCodingChallenge.Controllers;
using DegreedCodingChallenge.Interfaces;
using DegreedCodingChallenge.Interfaces.Clients;
using DegreedCodingChallenge.Services;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DegreedCodingChallenge.UnitTests
{
    [TestClass]
    public class JokeControllerTests
    {
        private JokeController _ctrl;
        private Mock<ILogger<JokeController>> _mockLogger;
        private Mock<IJokeService> _mockJokeService;

        [TestInitialize]
        public void Init()
        {
            _mockJokeService = new Mock<IJokeService>();

            _mockJokeService.Setup(j => j.GetRandomJoke()).Returns(() => Task.FromResult("random joke"));
            _mockJokeService.Setup(j => j.SearchJokes(It.IsAny<string>())).Returns(() => Task.FromResult(new Dictionary<string, List<string>>()));

            _mockLogger = new Mock<ILogger<JokeController>>();

            _ctrl = new JokeController(_mockLogger.Object, _mockJokeService.Object);
        }

        [TestMethod]
        public async Task ShouldCallRandomJoke()
        {
            var res = await _ctrl.Get();

            res.Should().Be("random joke");

            _mockJokeService.Verify(j => j.GetRandomJoke());
        }

        [TestMethod]
        public async Task ShouldSearchJokes()
        {
            var res = await _ctrl.Search("dad");

            _mockJokeService.Verify(x => x.SearchJokes("dad"), Times.Once);

        }

    }
}
