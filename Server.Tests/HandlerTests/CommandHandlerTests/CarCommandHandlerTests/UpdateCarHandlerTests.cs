using AutoFixture;
using AutoMapper;
using Contracts;
using Entities.Models;
using Moq;
using Server.Commands.CarCommands;
using Server.Exeptions;
using Server.Handlers.CommandHandlers.CarCommandHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Server.Tests.HandlerTests.CommandHandlerTests.CarCommandHandlerTests
{
    public class UpdateCarHandlerTests
    {
        private readonly Mock<IRepositoryManager> _repositoryMock;
        private readonly IFixture _fixture;
        private readonly CancellationToken _token;
        private readonly Mock<IMapper> _mapperMock;

        public UpdateCarHandlerTests()
        {
            _repositoryMock = new Mock<IRepositoryManager>();
            _fixture = new Fixture();
            _token = new CancellationToken();
            _mapperMock = new Mock<IMapper>();
        }

        [Fact]
        public async Task Handle_UpdateExistingCar_VerifyMapAndSaveMethods()
        {
            //Arrange
            var updateCarCommand = _fixture.Create<UpdateCarCommand>();

            var car = _fixture.Build<Car>()
                .With(c => c.Rent, default(Rent))
                .Create();

            _repositoryMock.Setup(c => c.Car.GetCarByIdAsync(updateCarCommand.Id, true, _token))
                .ReturnsAsync(car);

            _mapperMock.Setup(m => m.Map(updateCarCommand.carToUpdateDto, car));

            _repositoryMock.Setup(c => c.SaveAsync(_token));

            var updateCarHandler = new UpdateCarHandler(_repositoryMock.Object, _mapperMock.Object);

            //Act

            await updateCarHandler.Handle(updateCarCommand, _token);

            //Assert

            _repositoryMock.Verify(c => c.Car.GetCarByIdAsync(updateCarCommand.Id, true, _token), Times.Once);

            _repositoryMock.Verify(c => c.SaveAsync(_token), Times.Once);

            _mapperMock.Verify(c => c.Map(updateCarCommand.carToUpdateDto, car), Times.Once);
        }


        [Fact]
        public async Task Handle_UpdateNonExistingCar_ThrowNotFoundExeption()
        {
            //Arrange
            var updateCarCommand = _fixture.Build<UpdateCarCommand>()
                .With(c => c.Id, default(Guid))
                .Create();

            _repositoryMock.Setup(c => c.Car.GetCarByIdAsync(updateCarCommand.Id, true, _token))
                .ReturnsAsync(default(Car));

            var updateCarHandler = new UpdateCarHandler(_repositoryMock.Object, _mapperMock.Object);

            //Act

            var act = async () => await updateCarHandler.Handle(updateCarCommand, _token);

            //Assert

            await Assert.ThrowsAsync<NotFoundException>(act);
        }
    }
}
