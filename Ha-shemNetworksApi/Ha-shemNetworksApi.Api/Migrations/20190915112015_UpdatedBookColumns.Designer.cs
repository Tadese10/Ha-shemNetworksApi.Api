﻿// <auto-generated />
using System;
using Ha_shemNetworksApi.Api;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Ha_shemNetworksApi.Api.Migrations
{
    [DbContext(typeof(ApiDbContext))]
    [Migration("20190915112015_UpdatedBookColumns")]
    partial class UpdatedBookColumns
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Ha_shemNetworksApiCommon.Entities.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Author");

                    b.Property<bool>("Available");

                    b.Property<DateTime?>("BookDate");

                    b.Property<string>("ISBN");

                    b.Property<bool>("Status");

                    b.Property<string>("Title");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.ToTable("Books");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Author = "Marcel Proust",
                            Available = true,
                            ISBN = "",
                            Status = false,
                            Title = "In Search of Lost Time"
                        },
                        new
                        {
                            Id = 2,
                            Author = "Miguel de Cervantes",
                            Available = true,
                            ISBN = "",
                            Status = false,
                            Title = "Don Quixote"
                        });
                });

            modelBuilder.Entity("Ha_shemNetworksApiCommon.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Password");

                    b.Property<string>("Role");

                    b.Property<string>("Token");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FirstName = "Admin",
                            LastName = "Admin",
                            Password = "1000000.h+7q5Uy2L4OoJC/xRWrBBw==.BwmBKEGc6VV1w/xKoCQNlLFbdxJ2NBJ7m0VKoPCmTIU=",
                            Role = "Admin",
                            Username = "Admin"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}