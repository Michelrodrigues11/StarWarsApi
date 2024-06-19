using JediApi.Models;
using JediApi.Repositories;
using JediApi.Services;
using Moq;

namespace JediApi.Tests.Services
{
    public class JediServiceTests
    {
        // não mexer
        private readonly JediService _service;
        private readonly Mock<IJediRepository> _repositoryMock;

        public JediServiceTests()
        {
            // não mexer
            _repositoryMock = new Mock<IJediRepository>();
            _service = new JediService(_repositoryMock.Object);
        }

        [Fact]
        public async Task GetById_Success()
        {

            var idJedi = 1;
            var expectedJedi = new Jedi { Id = idJedi, Name = "NOME" };
            _repositoryMock.Setup(repo => repo.GetByIdAsync(idJedi)).ReturnsAsync(expectedJedi);
            var result = await _service.GetByIdAsync(idJedi);


            Assert.NotNull(result);
            Assert.Equal(expectedJedi.Id, result.Id);
            Assert.Equal(expectedJedi.Name, result.Name);
        }

        [Fact]
        public async Task GetById_NotFound()
        {

            var idJedi = 1;
            _repositoryMock.Setup(repo => repo.GetByIdAsync(idJedi)).ReturnsAsync((Jedi)null);


            var result = await _service.GetByIdAsync(idJedi);


            Assert.Null(result);
        }

        [Fact]
        public async Task GetAll()
        {

            var jediList = new List<Jedi>
            {
                new Jedi { Id = 1, Name = "NOME1" },
                new Jedi { Id = 2, Name = "NOME2" }
            };

            _repositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(jediList);


            var result = await _service.GetAllAsync();


            Assert.NotNull(result);
            Assert.Equal(jediList.Count, result.Count);
            Assert.Equal(jediList[0].Id, result[0].Id);
            Assert.Equal(jediList[1].Id, result[1].Id);
        }
    }
}
