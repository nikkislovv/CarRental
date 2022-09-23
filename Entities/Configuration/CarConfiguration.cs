using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Configuration
{
    public class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.HasData(
            new Car
            {
                Id = new Guid("80abbca8-664d-4b20-b5de-024705497d4a"),
                Name = "Porsche Panamera 970",
            },
            new Car
            {
                Id = new Guid("80abbca8-664d-4b20-b7de-024715497d4a"),

                Name = "BMW 7 серия G12 (Long)",
            },
            new Car
            {
                Id = new Guid("80abbca1-664d-4b70-b5de-024705497d4a"),

                Name = "Mercedes-Benz C-Класс AMG W202",
            }
            );
        }
    }
}
