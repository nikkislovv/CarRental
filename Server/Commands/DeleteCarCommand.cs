using MediatR;

namespace Server.Commands
{
    public class DeleteCarCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }

        public DeleteCarCommand(Guid id)
        {
            Id = id;
        }
    }
}
