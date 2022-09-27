using FluentValidation;
using Server.Commands.CarCommands;

namespace Server.Validation
{
    public class CarToCreateDtoValidator : AbstractValidator<CreateCarCommand>
    {
        public CarToCreateDtoValidator()
        {
            RuleFor(x => x.CarToCreate.Name).NotEmpty().MaximumLength(30);
        }
    }
}
