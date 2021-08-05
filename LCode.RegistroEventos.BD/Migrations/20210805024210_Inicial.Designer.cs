﻿// <auto-generated />
using System;
using LCode.RegistroEventos.BD;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LCode.RegistroEventos.BD.Migrations
{
    [DbContext(typeof(Contexto))]
    [Migration("20210805024210_Inicial")]
    partial class Inicial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("lcode")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LCode.NETCore.Base._5._0.Entidades.AplicativoComponente", b =>
                {
                    b.Property<int>("IdAplicativoComponente")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("FechaHoraRegistro")
                        .HasColumnType("datetime");

                    b.Property<string>("NombreComponente")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<string>("NombreComponenteCompleto")
                        .IsRequired()
                        .HasColumnType("varchar(500)");

                    b.HasKey("IdAplicativoComponente");

                    b.ToTable("AplicacionesComponentes");
                });

            modelBuilder.Entity("LCode.NETCore.Base._5._0.Entidades.EventoEntidad", b =>
                {
                    b.Property<int>("IdEvento")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("FechaHoraEvento")
                        .HasColumnType("datetime");

                    b.Property<string>("IdActividad")
                        .HasColumnType("varchar(150)");

                    b.Property<int>("IdEventoOrigen")
                        .HasColumnType("int");

                    b.Property<string>("Mensaje")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MensajeAdicional")
                        .HasColumnType("text");

                    b.Property<string>("MensajeDetallado")
                        .HasColumnType("text");

                    b.Property<int>("TipoEvento")
                        .HasColumnType("int");

                    b.HasKey("IdEvento");

                    b.HasIndex("IdEventoOrigen");

                    b.ToTable("Eventos");
                });

            modelBuilder.Entity("LCode.NETCore.Base._5._0.Entidades.EventoOrigen", b =>
                {
                    b.Property<int>("IdEventoOrigen")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("EsDocker")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaHoraRegistro")
                        .HasColumnType("datetime");

                    b.Property<string>("IPOrigen")
                        .HasColumnType("varchar(25)");

                    b.Property<int>("IdAplicativoComponente")
                        .HasColumnType("int");

                    b.Property<string>("NombreHost")
                        .HasColumnType("varchar(25)");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.HasKey("IdEventoOrigen");

                    b.HasIndex("IdAplicativoComponente");

                    b.ToTable("EventosOrigenes");
                });

            modelBuilder.Entity("LCode.NETCore.Base._5._0.Entidades.RastroEntidad", b =>
                {
                    b.Property<int>("IdRastro")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("FechaHoraRastro")
                        .HasColumnType("datetime");

                    b.Property<int>("IdEvento")
                        .HasColumnType("int");

                    b.Property<string>("NombreArchivo")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<string>("NombreClase")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("NombreDll")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<string>("NombreMetodo")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<int>("NumeroColumna")
                        .HasColumnType("int");

                    b.Property<int>("NumeroLinea")
                        .HasColumnType("int");

                    b.HasKey("IdRastro");

                    b.HasIndex("IdEvento");

                    b.ToTable("RastrosEventos");
                });

            modelBuilder.Entity("LCode.NETCore.Base._5._0.Entidades.EventoEntidad", b =>
                {
                    b.HasOne("LCode.NETCore.Base._5._0.Entidades.EventoOrigen", "EventoOrigen")
                        .WithMany("ListaEventos")
                        .HasForeignKey("IdEventoOrigen")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EventoOrigen");
                });

            modelBuilder.Entity("LCode.NETCore.Base._5._0.Entidades.EventoOrigen", b =>
                {
                    b.HasOne("LCode.NETCore.Base._5._0.Entidades.AplicativoComponente", "AplicativoComponente")
                        .WithMany("ListaOrigen")
                        .HasForeignKey("IdAplicativoComponente")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AplicativoComponente");
                });

            modelBuilder.Entity("LCode.NETCore.Base._5._0.Entidades.RastroEntidad", b =>
                {
                    b.HasOne("LCode.NETCore.Base._5._0.Entidades.EventoEntidad", "EventoEntidad")
                        .WithMany("ListaRastros")
                        .HasForeignKey("IdEvento")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EventoEntidad");
                });

            modelBuilder.Entity("LCode.NETCore.Base._5._0.Entidades.AplicativoComponente", b =>
                {
                    b.Navigation("ListaOrigen");
                });

            modelBuilder.Entity("LCode.NETCore.Base._5._0.Entidades.EventoEntidad", b =>
                {
                    b.Navigation("ListaRastros");
                });

            modelBuilder.Entity("LCode.NETCore.Base._5._0.Entidades.EventoOrigen", b =>
                {
                    b.Navigation("ListaEventos");
                });
#pragma warning restore 612, 618
        }
    }
}
