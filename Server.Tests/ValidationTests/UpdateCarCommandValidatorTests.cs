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
    public class UpdateCarCommandValidatorTests
    {
        private readonly UpdateCarCommandValidator _validator;

        public UpdateCarCommandValidatorTests()
        {
            _validator = new UpdateCarCommandValidator();
        }

        [Theory]
        [InlineData("Nissan X-Trail II (T31)", true)]
        [InlineData("Nissan X-Trail II (T32) 5454324232434", false)]
        [InlineData(null, false)]
        public void TestUpdateCarCommandValidation(string name, bool isValid)
        {
            var carToUpdateDto = new CarToUpdateDto()
            {
                Name = name
            };

            var updateCarCommand = new UpdateCarCommand(carToUpdateDto, Guid.Empty);

            Assert.Equal(isValid, _validator.Validate(updateCarCommand).IsValid);
        }
    }
}
