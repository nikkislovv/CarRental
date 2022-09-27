using FluentValidation;
using Server.Commands.CarCommands;

namespace Server.Validation
{
    public class UpdateCarCommandValidator : AbstractValidator<UpdateCarCommand>
    {
        public UpdateCarCommandValidator()
        {
            RuleFor(x => x.carToUpdateDto.Name).NotEmpty().MaximumLength(30);
        }
    }
}
