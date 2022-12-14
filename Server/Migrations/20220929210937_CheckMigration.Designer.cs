// <auto-generated />
using System;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Server.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    [Migration("20220929210937_CheckMigration")]
    partial class CheckMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Entities.Models.Car", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Cars");

                    b.HasData(
                        new
                        {
                            Id = new Guid("80abbca8-664d-4b20-b5de-024705497d4a"),
                            IsAvailable = true,
                            Name = "Porsche Panamera 970"
                        },
                        new
                        {
                            Id = new Guid("80abbca8-664d-4b20-b7de-024715497d4a"),
                            IsAvailable = true,
                            Name = "BMW 7 серия G12 (Long)"
                        },
                        new
                        {
                            Id = new Guid("80abbca1-664d-4b70-b5de-024705497d4a"),
                            IsAvailable = true,
                            Name = "Mercedes-Benz C-Класс AMG W202"
                        });
                });

            modelBuilder.Entity("Entities.Models.Rent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CarId")
                        .HasColumnType("uuid");

                    b.Property<string>("ClientName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CarId")
                        .IsUnique();

                    b.ToTable("Rents");
                });

            modelBuilder.Entity("Entities.Models.Rent", b =>
                {
                    b.HasOne("Entities.Models.Car", "Car")
                        .WithOne("Rent")
                        .HasForeignKey("Entities.Models.Rent", "CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");
                });

            modelBuilder.Entity("Entities.Models.Car", b =>
                {
                    b.Navigation("Rent")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
