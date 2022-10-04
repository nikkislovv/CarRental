using AutoFixture;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects.CarDTO;
using Entities.Models;
using Moq;
using Server.Exeptions;
using Server.Handlers.QueryHandlers.CarHandlers;
using Server.Queries.CarQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Server.Tests.HandlerTests.QueryHandlerTests.CarQueryHandlerTests
{
    public class GetCarByIdHandlerTests
    {
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IRepositoryManager> _repositoryMock;
        private readonly IFixture _fixture;
        private readonly CancellationToken _token;

        public GetCarByIdHandlerTests()
        {
            _mapperMock = new Mock<IMapper>();
            _repositoryMock = new Mock<IRepositoryManager>();
            _token = new CancellationToken();
            _fixture = new Fixture();
        }

        [Fact]
        public async Task Handle_GetExistingCar_ReturnsCarDto()
        {
            //Arrange
            var car = _fixture.Build<Car>()
                .With(c => c.Rent, default(Rent))
                .Create();

            var getCarByIdQuery = _fixture.Build<GetCarByIdQuery>()
                .With(c => c.trackChanges, false)
                .With(c => c.Id, car.Id)
                .Create();

            var carDto = _fixture.Create<CarToShowDto>();

            _repositoryMock.Setup(c => c.Car.GetCarByIdAsync(getCarByIdQuery.Id, getCarByIdQuery.trackChanges, _token))
                .ReturnsAsync(car);

            _mapperMock.Setup(m => m.Map<CarToShowDto>(car))
                .Returns(carDto);

            var getCarByIdHandler = new GetCarByIdHandler(_repositoryMock.Object, _mapperMock.Object);

            //Act

            var result = await getCarByIdHandler.Handle(getCarByIdQuery, _token);

            //Assert

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Handle_GetNonExistingCar_ThrowNotFoundExeption()
        {
            //Arrange
            var getCarByIdQuery = _fixture.Build<GetCarByIdQuery>()
                            .With(c => c.trackChanges, false)
                            .Create();

            _repositoryMock.Setup(c => c.Car.GetCarByIdAsync(getCarByIdQuery.Id, getCarByIdQuery.trackChanges, _token))
                .ReturnsAsync(default(Car));

            var getCarByIdHandler = new GetCarByIdHandler(_repositoryMock.Object, _mapperMock.Object);

            //Act

            var act = async () => await getCarByIdHandler.Handle(getCarByIdQuery, _token);

            //Assert

            await Assert.ThrowsAsync<NotFoundException>(act);
        }
    }
}
