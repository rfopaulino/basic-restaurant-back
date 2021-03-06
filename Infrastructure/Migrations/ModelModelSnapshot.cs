﻿// <auto-generated />
using System;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    [DbContext(typeof(Model))]
    partial class ModelModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-preview2-30571")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.Entity.Prato", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<int>("IdRestaurante")
                        .HasColumnName("id_restaurante");

                    b.Property<string>("NomePrato")
                        .HasColumnName("nome_prato");

                    b.Property<decimal>("Preco")
                        .HasColumnName("preco");

                    b.HasKey("Id");

                    b.HasIndex("IdRestaurante");

                    b.ToTable("Prato");
                });

            modelBuilder.Entity("Domain.Entity.Restaurante", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("Nome")
                        .HasColumnName("nome");

                    b.HasKey("Id");

                    b.ToTable("Restaurante");
                });

            modelBuilder.Entity("Domain.Entity.Prato", b =>
                {
                    b.HasOne("Domain.Entity.Restaurante", "Restaurante")
                        .WithMany("Prato")
                        .HasForeignKey("IdRestaurante")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
