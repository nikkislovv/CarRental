using AutoFixture;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects.RentDTO;
using Entities.Models;
using Moq;
using Server.Exeptions;
using Server.Handlers.QueryHandlers.RentQueryHandlers;
using Server.Queries.RentQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Server.Tests.HandlerTests.QueryHandlerTests.RentQueryHandlerTests
{
    public class GetRentByIdHanlerTests
    {
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IRepositoryManager> _repositoryMock;
        private readonly IFixture _fixture;
        private readonly CancellationToken _token;

        public GetRentByIdHanlerTests()
        {
            _mapperMock = new Mock<IMapper>();
            _repositoryMock = new Mock<IRepositoryManager>();
            _token = new CancellationToken();
            _fixture = new Fixture();
        }

        [Fact]
        public async Task Handle_GetExistingRent_ReturnsRentDto()
        {
            //Arrange
            var rent = _fixture.Build<Rent>()
                .With(r => r.Car, new Car())
                .Create();

            var getRentByIdQuery = _fixture.Build<GetRentByIdQuery>()
                .With(r => r.Id, rent.Id)
                .With(r => r.trackChanges, true)
                .Create();

            var rentDto = _fixture.Create<RentToShowDto>();

            _repositoryMock.Setup(r => r.Rent.GetRentByIdAsync(getRentByIdQuery.Id, getRentByIdQuery.trackChanges, _token))
                .ReturnsAsync(rent);

            _mapperMock.Setup(r => r.Map<RentToShowDto>(rent))
                .Returns(rentDto);

            var getRentByIdHandler = new GetRentByIdHandler(_repositoryMock.Object, _mapperMock.Object);

            //Act

            var result = getRentByIdHandler.Handle(getRentByIdQuery, _token);

            //Assert

            Assert.NotNull(result);

        }

        [Fact]
        public async Task Handle_GetNonExistingRent_ThrowNotFoundExeption()
        {
            //Arrange

            var getRentByIdQuery = _fixture.Build<GetRentByIdQuery>()
                .With(r => r.trackChanges, true)
                .Create();

            _repositoryMock.Setup(r => r.Rent.GetRentByIdAsync(getRentByIdQuery.Id, getRentByIdQuery.trackChanges, _token))
                .ReturnsAsync(default(Rent));

            var getRentByIdHandler = new GetRentByIdHandler(_repositoryMock.Object, _mapperMock.Object);

            //Act

            var act = async () => await getRentByIdHandler.Handle(getRentByIdQuery, _token);

            //Assert

            await Assert.ThrowsAsync<NotFoundException>(act);
        }

    }
}
