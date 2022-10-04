using AutoFixture;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects.CarDTO;
using Entities.Models;
using Moq;
using Server.Commands.CarCommands;
using Server.Handlers.CommandHandlers.CarCommandHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;


namespace Server.Tests.HandlerTests.CommandHandlerTests.CarCommandHandlerTests
{
    public class CreateCarHandlerTests
    {
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IRepositoryManager> _repositoryMock;
        private readonly IFixture _fixture;
        private readonly CancellationToken _token;
        public CreateCarHandlerTests()
        {
            _mapperMock = new Mock<IMapper>();
            _repositoryMock = new Mock<IRepositoryManager>();
            _fixture = new Fixture();
            _token = new CancellationToken();
        }

        [Fact]
        public async Task Handle_CreateCar_ResultCarIdNotEmpty()
        {
            //Arrange
            var createCarCommand = _fixture.Create<CreateCarCommand>();

            var car = _fixture.Build<Car>()
                .With(car => car.Rent, default(Rent))
                .Create();

            _mapperMock
                .Setup(m => m.Map<Car>(createCarCommand.CarToCreate))
                .Returns(car);

            _repositoryMock
                .Setup(r => r.Car.CreateCar(car));

            _repositoryMock.Setup(r => r.SaveAsync(_token));

            var createCarHandler = new CreateCarHandler(_repositoryMock.Object, _mapperMock.Object);

            //Act

            var result = await createCarHandler.Handle(createCarCommand, _token);

            //Assert

            Assert.NotEqual(Guid.Empty, result);
        }
    }
}
