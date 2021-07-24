using DegreedCodingChallenge.Interfaces.Clients;
using DegreedCodingChallenge.Services;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DegreedCodingChallenge.UnitTests
{
    [TestClass]
    public class JokeServiceTests
    {

        private Mock<ICanHazDadJokeClient> _mockClient;
        private JokeService _svc;

        [TestInitialize]
        public void Init()
        {
            _mockClient = new Mock<ICanHazDadJokeClient>();

            _mockClient.Setup(x => x.GetRandomJoke())
                .Returns(() => Task.FromResult("This is a random joke"));

            _mockClient.Setup(x => x.SearchJokes(It.IsAny<string>()))
                .Returns((string searchTerm) => Task.FromResult(new List<string>()
                {
                    $"This is a short {searchTerm} joke",
                    $"This is a medium {searchTerm} joke with a few more words in it",
                    $"This is a long {searchTerm} joke with a whole lot of words in it because sometimes jokes are funnier when they go on forever"
                }));



            _svc = new JokeService(_mockClient.Object);
        }

        [TestMethod]
        public async Task ShouldReturnRandomJoke()
        {
            var resp = await _svc.GetRandomJoke();

            resp.Should().Be("This is a random joke");

            _mockClient.Verify(x => x.GetRandomJoke(), Times.Once);
        }

        [TestMethod]
        public async Task ShouldSearchJokes()
        {
            var resp = await _svc.SearchJokes("Dad");

            resp.Should().NotBeNull();
            resp.Should().BeOfType<Dictionary<string, List<string>>>();

            resp.Keys.Count.Should().Be(3);

            resp.Keys.Should().Contain("Short");
            resp.Keys.Should().Contain("Medium");
            resp.Keys.Should().Contain("Long");


            resp["Short"].Count.Should().Be(1);
            resp["Short"].First().Should().Be("This is a short ||Dad|| joke");


            resp["Medium"].Count.Should().Be(1);
            resp["Medium"].First().Should().Be("This is a medium ||Dad|| joke with a few more words in it");


            resp["Long"].Count.Should().Be(1);
            resp["Long"].First().Should().Be("This is a long ||Dad|| joke with a whole lot of words in it because sometimes jokes are funnier when they go on forever");
        }
    }
}
