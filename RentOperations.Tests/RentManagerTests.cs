using AutoFixture;
using Contracts;
using Entities.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RentOperations.Tests
{
    public class RentManagerTests
    {
        private readonly Mock<IRepositoryManager> _repositoryMock;
        private readonly IFixture _fixture;
        private readonly CancellationToken _token;

        public RentManagerTests()
        {
            _repositoryMock = new Mock<IRepositoryManager>();
            _fixture = new Fixture();
            _token = new CancellationToken();
        }

        [Fact]
        public async Task RentCar_RentExistingCar_ResultCarIsAvailableFalse()
        {
            //Arrange
            var car = _fixture.Build<Car>()
                .With(c => c.IsAvailable, true)
                .With(c => c.Rent, default(Rent))
                .Create();

            _repositoryMock.Setup(c => c.Car.GetCarByIdAsync(car.Id, false, _token))
                .ReturnsAsync(car);

            var rentManager = new RentManager(_repositoryMock.Object);

            //Act

            await rentManager.RentCar(car.Id, _token);

            //Assert

            Assert.False(car.IsAvailable);

        }
    }
}
