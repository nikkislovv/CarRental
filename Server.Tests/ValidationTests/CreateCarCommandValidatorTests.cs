using Entities.DataTransferObjects.CarDTO;
using Server.Commands.CarCommands;
using Server.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Server.Tests.ValidationTests
{
    public class CreateCarCommandValidatorTests
    {
        private readonly CreateCarCommandValidator _validator;

        public CreateCarCommandValidatorTests()
        {
            _validator = new CreateCarCommandValidator();
        }

        [Theory]
        [InlineData("Nissan X-Trail II (T32)", true)]
        [InlineData("Nissan X-Trail II (T32) 5454324232434", false)]
        [InlineData(null, false)]
        public void TestCreateCarCommandValidation(string name, bool isValid)
        {
            var carToCreateDto = new CarToCreateDto
            {
                Name = name
            };

            var createCarCommand = new CreateCarCommand(carToCreateDto);

            Assert.Equal(isValid, _validator.Validate(createCarCommand).IsValid);
        }
    }
}
