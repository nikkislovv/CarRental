using AutoFixture;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects.RentDTO;
using Entities.Models;
using Moq;
using Server.Commands.RentCommands;
using Server.Exeptions;
using Server.Handlers.CommandHandlers.CarCommandHandlers;
using Server.Handlers.CommandHandlers.RentCommandHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Server.Tests.HandlerTests.CommandHandlerTests.RentCommandHandlerTests
{
    public class CreateRentHandlerTests
    {
        private readonly Mock<IRepositoryManager> _repositoryMock;
        private readonly IFixture _fixture;
        private readonly CancellationToken _token;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IRentManager> _rentMangerMock;

        public CreateRentHandlerTests()
        {
            _repositoryMock = new Mock<IRepositoryManager>();
            _fixture = new Fixture();
            _token = new CancellationToken();
            _rentMangerMock = new Mock<IRentManager>();
            _mapperMock = new Mock<IMapper>();
        }

        [Fact]
        public async Task Handle_CreateRentWithExistingCar_ResultRentIdNotEmpty()
        {
            //Arrange
            var car = _fixture.Build<Car>()
                .With(c => c.Rent, default(Rent))
                .Create();

            var createRentCommand = _fixture.Build<CreateRentCommand>()
                .With(r => r.RentToCreateDto, _fixture.Build<RentToCreateDto>()
                    .With(r => r.CarId, car.Id).Create())
                .Create();

            var rent = _fixture.Build<Rent>()
                .With(r => r.Car, car)
                .Create();

            _repositoryMock.Setup(r => r.Car.GetCarByIdAsync(createRentCommand.RentToCreateDto.CarId, false, _token))
                .ReturnsAsync(car);

            _mapperMock.Setup(r => r.Map<Rent>(createRentCommand.RentToCreateDto))
                .Returns(rent);

            _repositoryMock.Setup(r => r.Rent.CreateRent(rent));

            _rentMangerMock.Setup(r => r.RentCar(createRentCommand.RentToCreateDto.CarId, _token));

            _repositoryMock.Setup(r => r.SaveAsync(_token));

            var createRentHandler = new CreateRentHandler(_repositoryMock.Object, _mapperMock.Object, _rentMangerMock.Object);

            //Act

            var result = await createRentHandler.Handle(createRentCommand, _token);

            //Assert

            Assert.NotEqual(Guid.Empty, result);
        }

        [Fact]
        public async Task Handle_CreateRentWithNonExistingCar_ThrowNotFoundExeption()
        {
            //Arrange
            var createRentCommand = _fixture.Build<CreateRentCommand>()
             .With(r => r.RentToCreateDto, _fixture.Build<RentToCreateDto>()
                 .With(r => r.CarId, default(Guid)).Create())
             .Create();

            _repositoryMock.Setup(r => r.Car.GetCarByIdAsync(createRentCommand.RentToCreateDto.CarId, false, _token))
                .ReturnsAsync(default(Car));

            var createRentHandler = new CreateRentHandler(_repositoryMock.Object, _mapperMock.Object, _rentMangerMock.Object);

            //Act

            var act = async () => await createRentHandler.Handle(createRentCommand, _token);

            //Assert

            await Assert.ThrowsAsync<NotFoundException>(act);
        }

    }
}
