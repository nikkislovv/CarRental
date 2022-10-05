using Entities.DataTransferObjects.RentDTO;
using Server.Commands.RentCommands;
using Server.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Server.Tests.ValidationTests
{
    public class CreateRentCommandValidatorTests
    {
        private readonly CreateRentCommandValidator _validator;

        public CreateRentCommandValidatorTests()
        {
            _validator = new CreateRentCommandValidator();
        }

        [Theory]
        [InlineData("Nikita Kislov", "24bf9e81-d245-43f1-85f4-662353dcb233", true)]
        [InlineData("Nikita Kislovvvvvvvvvvvvvvvvvvvvv", "24bf9e81-d245-43f1-85f4-662353dcb233", false)]
        [InlineData(null, "24bf9e81-d245-43f1-85f4-662353dcb233", false)]
        [InlineData("Nikita Kislov", "00000000-0000-0000-0000-000000000000", false)]
        public void TestCreateRentCommandValidation(string clientName, Guid carId, bool isValid)
        {
            var rentToCreateDto = new RentToCreateDto()
            {
                ClientName = clientName,
                CarId = carId
            };

            var createRentCommand = new CreateRentCommand(rentToCreateDto);

            Assert.Equal(isValid, _validator.Validate(createRentCommand).IsValid);
        }
    }
}
