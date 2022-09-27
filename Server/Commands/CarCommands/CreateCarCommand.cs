﻿using Entities.DataTransferObjects.CarDTO;
using MediatR;

namespace Server.Commands.CarCommands
{
    public class CreateCarCommand : IRequest<CarToShowDto>
    {
        public CarToCreateDto CarToCreate { get; set; }

        public CreateCarCommand(CarToCreateDto carToCreate)
        {
            CarToCreate = carToCreate;
        }
    }
}
