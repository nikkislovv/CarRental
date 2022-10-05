using FluentValidation;
using Server.Commands.CarCommands;

namespace Server.Validation
{
    public class CreateCarCommandValidator : AbstractValidator<CreateCarCommand>
    {
        public CreateCarCommandValidator()
        {
            RuleFor(x => x.CarToCreate.Name).NotEmpty().MaximumLength(30);
        }
    }
}
