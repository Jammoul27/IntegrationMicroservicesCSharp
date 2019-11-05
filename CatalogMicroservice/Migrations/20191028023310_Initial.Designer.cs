﻿// <auto-generated />
using CatalogMicroservice.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CatalogMicroservice.Migrations
{
    [DbContext(typeof(PolicyContext))]
    [Migration("20191028023310_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CatalogMicroservice.Models.InsurancePolicy", b =>
                {
                    b.Property<long>("PolicyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("BasePrice")
                        .HasColumnType("float");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PolicyId");

                    b.ToTable("InsurancePolicies");

                    b.HasData(
                        new
                        {
                            PolicyId = 1L,
                            BasePrice = 106040.0,
                            Description = "This is the most basic policy, for families of size 1-3",
                            Name = "Miniature Policy"
                        },
                        new
                        {
                            PolicyId = 2L,
                            BasePrice = 87640.0,
                            Description = "This is a policy for a family of size 4-6",
                            Name = "Small Sized Family Policy"
                        },
                        new
                        {
                            PolicyId = 3L,
                            BasePrice = 76810.0,
                            Description = "This policy is for families consiting of 7-9 members",
                            Name = "Medium Sized Family Policy"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
