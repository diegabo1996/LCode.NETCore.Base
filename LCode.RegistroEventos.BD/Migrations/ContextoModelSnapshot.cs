﻿// <auto-generated />
using System;
using LCode.RegistroEventos.BD;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LCode.RegistroEventos.BD.Migrations
{
    [DbContext(typeof(Contexto))]
    partial class ContextoModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("lcode")
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7");

            modelBuilder.Entity("LCode.RegistroEventos.BD.Modelos.NuevoEvento", b =>
                {
                    b.Property<int>("IdRegistroEvento")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool>("EsDocker")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaHoraEvento")
                        .HasColumnType("datetime");

                    b.Property<string>("IPOrigen")
                        .HasColumnType("varchar(25)");

                    b.Property<string>("IdActividad")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Mensaje")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MensajeAdicional")
                        .HasColumnType("text");

                    b.Property<string>("MensajeDetallado")
                        .HasColumnType("text");

                    b.Property<string>("NombreClase")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("NombreComponente")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<string>("NombreComponenteCompleto")
                        .IsRequired()
                        .HasColumnType("varchar(500)");

                    b.Property<string>("NombreMetodo")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<int>("NumeroColumna")
                        .HasColumnType("int");

                    b.Property<int>("NumeroLinea")
                        .HasColumnType("int");

                    b.Property<int>("TipoEvento")
                        .HasColumnType("int");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.HasKey("IdRegistroEvento");

                    b.ToTable("Eventos");
                });
#pragma warning restore 612, 618
        }
    }
}
