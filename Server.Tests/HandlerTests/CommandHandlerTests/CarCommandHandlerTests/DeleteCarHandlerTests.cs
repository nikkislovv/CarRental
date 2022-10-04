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
using AutoFixture;

namespace Server.Tests.HandlerTests.CommandHandlerTests.CarCommandHandlerTests
{
    public class DeleteCarHandlerTests
    {
        private readonly Mock<IRepositoryManager> _repositoryMock;
        private readonly IFixture _fixture;
        private readonly CancellationToken _token;

        public DeleteCarHandlerTests()
        {
            _repositoryMock = new Mock<IRepositoryManager>();
            _fixture = new Fixture();
            _token = new CancellationToken();
        }

        [Fact]
        public async Task Handle_DeleteExistingCar_VerifyDeleteAndSaveMethods()
        {
            //Arrange
            var deleteCarCommand = _fixture.Create<DeleteCarCommand>();

            var car = _fixture.Build<Car>()
                .With(car => car.Rent, default(Rent))
                .Create();

            _repositoryMock
                .Setup(c => c.Car.GetCarByIdAsync(deleteCarCommand.Id, false, _token))
                .ReturnsAsync(car);

            _repositoryMock
                .Setup(c => c.Car.DeleteCar(car));

            _repositoryMock
                .Setup(c => c.SaveAsync(_token));

            var deleteCarHandler = new DeleteCarHandler(_repositoryMock.Object);
            //Act

            await deleteCarHandler.Handle(deleteCarCommand, _token);

            //Assert

            _repositoryMock.Verify(c => c.Car.GetCarByIdAsync(deleteCarCommand.Id, false, _token), Times.Once);

            _repositoryMock.Verify(c => c.Car.DeleteCar(car), Times.Once);

            _repositoryMock.Verify(c => c.SaveAsync(_token), Times.Once);
        }


        [Fact]
        public async Task Handle_DeleteNonExistingCar_ThrowNotFoundExeption()
        {
            //Arrange
            var deleteCarCommand = _fixture.Build<DeleteCarCommand>()
                .With(c => c.Id, default(Guid))
                .Create();
                
            _repositoryMock
                .Setup(c => c.Car.GetCarByIdAsync(deleteCarCommand.Id, false, _token))
                .ReturnsAsync(default(Car));

            var deleteCarHandler = new DeleteCarHandler(_repositoryMock.Object);
            //Act

            var act = async () => await deleteCarHandler.Handle(deleteCarCommand, _token);

            //Assert

            await Assert.ThrowsAsync<NotFoundException>(act);
        }
    }
}
