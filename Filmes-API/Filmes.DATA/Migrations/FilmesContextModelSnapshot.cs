﻿// <auto-generated />
using System;
using Filmes.DATA.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Filmes.DATA.Migrations
{
    [DbContext(typeof(FilmesContext))]
    partial class FilmesContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Filmes.DOMAIN.Entity.All.Diretor", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long>("GeneroId")
                        .HasColumnType("bigint");

                    b.Property<string>("Nacionalidade")
                        .HasColumnType("text");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("character varying(45)");

                    b.HasKey("Id");

                    b.HasIndex("GeneroId");

                    b.ToTable("Diretor", "All");
                });

            modelBuilder.Entity("Filmes.DOMAIN.Entity.All.Filme", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Avaliacoes")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("DataLancamento")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Genero")
                        .HasColumnType("text");

                    b.Property<byte[]>("Poster")
                        .HasColumnType("bytea");

                    b.Property<string>("Sinopse")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("character varying(45)");

                    b.HasKey("Id");

                    b.ToTable("Filme", "All");
                });

            modelBuilder.Entity("Filmes.DOMAIN.Entity.All.FilmeDiretor", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("DiretorId")
                        .HasColumnType("bigint");

                    b.Property<long>("FilmeId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("FilmeId");

                    b.HasIndex("DiretorId", "FilmeId")
                        .IsUnique();

                    b.ToTable("FilmeDiretor", "All");
                });

            modelBuilder.Entity("Filmes.DOMAIN.Entity.All.Genero", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<string>("Nome")
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)");

                    b.HasKey("Id");

                    b.ToTable("Genero", "All");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Nome = "Masculino"
                        },
                        new
                        {
                            Id = 2L,
                            Nome = "Feminimo"
                        });
                });

            modelBuilder.Entity("Filmes.DOMAIN.Entity.All.PerfilUsuario", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<string>("Nome")
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)");

                    b.Property<string>("Role")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("Id");

                    b.ToTable("PerfilUsuario", "All");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Nome = "Master",
                            Role = "Master"
                        });
                });

            modelBuilder.Entity("Filmes.DOMAIN.Entity.All.Usuario", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("Ativo")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("DataHoraCriacao")
                        .HasColumnType("timestamp without time zone");

                    b.Property<byte[]>("Foto")
                        .HasColumnType("bytea");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("character varying(120)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("PerfilId")
                        .HasColumnType("bigint");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("TemaEscuro")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("Login")
                        .IsUnique();

                    b.HasIndex("PerfilId");

                    b.ToTable("Usuario", "All");
                });

            modelBuilder.Entity("Filmes.DOMAIN.Entity.All.Diretor", b =>
                {
                    b.HasOne("Filmes.DOMAIN.Entity.All.Genero", "Genero")
                        .WithMany()
                        .HasForeignKey("GeneroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genero");
                });

            modelBuilder.Entity("Filmes.DOMAIN.Entity.All.FilmeDiretor", b =>
                {
                    b.HasOne("Filmes.DOMAIN.Entity.All.Diretor", "Diretor")
                        .WithMany("Filmes")
                        .HasForeignKey("DiretorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Filmes.DOMAIN.Entity.All.Filme", "Filme")
                        .WithMany("Diretores")
                        .HasForeignKey("FilmeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Diretor");

                    b.Navigation("Filme");
                });

            modelBuilder.Entity("Filmes.DOMAIN.Entity.All.Usuario", b =>
                {
                    b.HasOne("Filmes.DOMAIN.Entity.All.PerfilUsuario", "Perfil")
                        .WithMany()
                        .HasForeignKey("PerfilId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Perfil");
                });

            modelBuilder.Entity("Filmes.DOMAIN.Entity.All.Diretor", b =>
                {
                    b.Navigation("Filmes");
                });

            modelBuilder.Entity("Filmes.DOMAIN.Entity.All.Filme", b =>
                {
                    b.Navigation("Diretores");
                });
#pragma warning restore 612, 618
        }
    }
}
