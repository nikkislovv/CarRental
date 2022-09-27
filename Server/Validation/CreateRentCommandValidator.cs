using FluentValidation;
using Server.Commands.RentCommands;

namespace Server.Validation
{
    public class CreateRentCommandValidator : AbstractValidator<CreateRentCommand>
    {
        public CreateRentCommandValidator()
        {
            RuleFor(x => x.RentToCreateDto.ClientName)
                .NotEmpty()
                .MaximumLength(30);
            RuleFor(x => x.RentToCreateDto.CarId)
                .NotEmpty();
                
        }

    }
}
